using System;
using System.Collections.Generic;
using System.Text;

using NotificationService.Domain.Entities;

namespace NotificationService.Application.Interfaces;

public interface INotificationRepository
{
    Task AddAsync(
        Notification notification,
        CancellationToken cancellationToken);

    Task<List<Notification>> GetByUserIdAsync(
        string userId,
        CancellationToken cancellationToken);
}
