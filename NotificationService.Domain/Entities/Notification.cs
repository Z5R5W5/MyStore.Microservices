using System;
using System.Collections.Generic;
using System.Text;

namespace NotificationService.Domain.Entities;

public class Notification
{
    public string Id { get; private set; } = string.Empty;

    public string UserId { get; private set; } = string.Empty;

    public int OrderId { get; private set; }

    public string Message { get; private set; } = string.Empty;

    public DateTime CreatedAtUtc { get; private set; }

    private Notification()
    {
    }

    public Notification(
        string userId,
        int orderId,
        string message)
    {
        Id = Guid.NewGuid().ToString();

        UserId = userId;
        OrderId = orderId;
        Message = message;
        CreatedAtUtc = DateTime.UtcNow;
    }
}