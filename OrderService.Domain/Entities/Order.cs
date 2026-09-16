using System;
using System.Collections.Generic;
using System.Text;

namespace OrderService.Domain.Entities;

public class Order
{
    public int Id { get; private set; }

    public string UserId { get; private set; } = string.Empty;

    public DateTime CreatedAtUtc { get; private set; }

    public OrderStatus Status { get; private set; }

    private readonly List<OrderItem> _items = [];

    public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();

    private Order()
    {
    }

    public Order(string userId)
    {
        UserId = userId;
        CreatedAtUtc = DateTime.UtcNow;
        Status = OrderStatus.Pending;
    }

    public void AddItem(
        int productId,
        int quantity,
        decimal unitPrice)
    {
        var item = new OrderItem(
            productId,
            quantity,
            unitPrice);

        _items.Add(item);
    }

    public void Cancel()
    {
        if (Status == OrderStatus.Cancelled)
            throw new InvalidOperationException(
                "Order is already cancelled.");

        Status = OrderStatus.Cancelled;
    }
}