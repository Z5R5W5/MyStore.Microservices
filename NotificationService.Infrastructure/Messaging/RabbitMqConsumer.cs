using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NotificationService.Application.Events;
using NotificationService.Application.Interfaces;
using NotificationService.Domain.Entities;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace NotificationService.Infrastructure.Messaging;

public class RabbitMqConsumer
{
    private readonly IConfiguration _configuration;
    private readonly IServiceScopeFactory _scopeFactory;


    private IConnection? _connection;
    private IChannel? _channel;

    public RabbitMqConsumer(
        IConfiguration configuration,
        IServiceScopeFactory scopeFactory)
    {
        _configuration = configuration;
        _scopeFactory = scopeFactory;
    }

    private ConnectionFactory CreateFactory()
    {
        return new ConnectionFactory
        {
            HostName =
                _configuration["RabbitMQ:Host"]
                ?? "localhost",

            Port =
                int.Parse(
                    _configuration["RabbitMQ:Port"]
                    ?? "5672"),

            UserName =
                _configuration["RabbitMQ:Username"]
                ?? "guest",

            Password =
                _configuration["RabbitMQ:Password"]
                ?? "guest"
        };
    }

    private async Task CreateConnectionAsync(
        CancellationToken cancellationToken)
    {
        var factory = CreateFactory();

        _connection =
            await factory.CreateConnectionAsync(
                cancellationToken);

        _channel =
            await _connection.CreateChannelAsync(
                cancellationToken: cancellationToken);
    }

    private async Task SetupTopologyAsync(
        CancellationToken cancellationToken)
    {
        if (_channel is null)
        {
            throw new InvalidOperationException(
                "RabbitMQ channel has not been created.");
        }

        var exchange =
            _configuration["RabbitMQ:Exchange"]
            ?? "orders";

        var queue =
            _configuration["RabbitMQ:Queue"]
            ?? "order-created";

        var routingKey =
            _configuration["RabbitMQ:RoutingKey"]
            ?? "order.created";

        await _channel.ExchangeDeclareAsync(
            exchange: exchange,
            type: ExchangeType.Direct,
            durable: true,
            autoDelete: false,
            cancellationToken: cancellationToken);

        await _channel.QueueDeclareAsync(
            queue: queue,
            durable: true,
            exclusive: false,
            autoDelete: false,
            cancellationToken: cancellationToken);

        await _channel.QueueBindAsync(
            queue: queue,
            exchange: exchange,
            routingKey: routingKey,
            cancellationToken: cancellationToken);
    }

    public async Task StartAsync(
        CancellationToken cancellationToken)
    {
        await CreateConnectionAsync(
            cancellationToken);

        await SetupTopologyAsync(
            cancellationToken);
        if (_channel is null)
        {
            throw new InvalidOperationException(
                "RabbitMQ channel has not been created.");
        }

        var queue =
            _configuration["RabbitMQ:Queue"]
            ?? "order-created";

        var consumer =
            new AsyncEventingBasicConsumer(_channel);

        consumer.ReceivedAsync += async (_, eventArgs) =>
        {
            try {
                var body =
                eventArgs.Body.ToArray();

                var json =
                    Encoding.UTF8.GetString(body);

                var message =
                    JsonSerializer.Deserialize<OrderCreatedEvent>(
                        json);

                if (message is null)
                {
                    Console.WriteLine(
                        "Failed to deserialize OrderCreatedEvent.");

                    await _channel.BasicNackAsync(
                        deliveryTag: eventArgs.DeliveryTag,
                        multiple: false,
                        requeue: false);

                    return;
                }

                Console.WriteLine(
                    $"Order received: {message.OrderId}");

                using var scope =
                        _scopeFactory.CreateScope();

                var repository =
                    scope.ServiceProvider
                        .GetRequiredService<INotificationRepository>();

                var notification =
                    new Notification(
                        message.UserId,
                        message.OrderId,
                        $"Your order #{message.OrderId} was created.");

                await repository.AddAsync(
                    notification,
                    CancellationToken.None);
                await _channel.BasicAckAsync(
                    deliveryTag: eventArgs.DeliveryTag,
                    multiple: false);

                Console.WriteLine(
                    $"Notification created for order {message.OrderId}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"Error processing message: {ex.Message}");
                await _channel.BasicNackAsync(
                    deliveryTag: eventArgs.DeliveryTag,
                    multiple: false,
                    requeue: true);
            }

        };

        await _channel.BasicConsumeAsync(
            queue: queue,
            autoAck: false,
            consumer: consumer,
            cancellationToken: cancellationToken);
    }
}