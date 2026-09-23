using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using OrderService.Application.Events;
using OrderService.Application.Interfaces;
using OrderService.Domain.Entities;

namespace OrderService.Application.Orders.Commands.CreateOrder;

public class CreateOrderCommandHandler
    : IRequestHandler<CreateOrderCommand, int>
{
    private readonly IProductServiceClient _productServiceClient;
    private readonly IOrderRepository _orderRepository;
    private readonly IMessagePublisher _messagePublisher;

    public CreateOrderCommandHandler(
        IProductServiceClient productServiceClient,
        IOrderRepository orderRepository,
        IMessagePublisher messagePublisher)
    {
        _productServiceClient = productServiceClient;
        _orderRepository = orderRepository;
        _messagePublisher = messagePublisher;
    }

    public async Task<int> Handle(
        CreateOrderCommand request,
        CancellationToken cancellationToken)
    {
        var order = new Order(request.UserId);

        foreach (var item in request.Items)
        {
            var product =
                await _productServiceClient.GetProductAsync(
                    item.ProductId,
                    cancellationToken);

            if (product is null)
            {
                throw new KeyNotFoundException(
                    $"Product with id {item.ProductId} was not found.");
            }

            if (item.Quantity <= 0)
            {
                throw new ArgumentException(
                    "Quantity must be greater than zero.");
            }

            if (product.Stock < item.Quantity)
            {
                throw new InvalidOperationException(
                    $"Product {product.Name} does not have enough stock.");
            }

            order.AddItem(
                product.Id,
                item.Quantity,
                product.Price);
        }

        await _orderRepository.AddAsync(
            order,
            cancellationToken);

        var orderCreatedEvent = new OrderCreatedEvent(
            order.Id,
            order.UserId);
        await _messagePublisher.PublishAsync(
                orderCreatedEvent,
                "orders",
                "order.created",
                cancellationToken);

        return order.Id;
    }
}


