using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.Hosting;

namespace NotificationService.Infrastructure.Messaging;

public class RabbitMqConsumerHostedService : BackgroundService
{
    private readonly RabbitMqConsumer _consumer;

    public RabbitMqConsumerHostedService(
        RabbitMqConsumer consumer)
    {
        _consumer = consumer;
    }

    protected override async Task ExecuteAsync(
        CancellationToken stoppingToken)
    {
        await _consumer.StartAsync(
            stoppingToken);

       try
        {
            await Task.Delay(
                Timeout.Infinite,
                stoppingToken);
        }
        catch (OperationCanceledException)
        {
            // Expected during shutdown.
        }
    }
}
