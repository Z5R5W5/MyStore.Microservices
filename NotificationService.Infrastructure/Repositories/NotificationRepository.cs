using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using NotificationService.Application.Interfaces;
using NotificationService.Domain.Entities;

namespace NotificationService.Infrastructure.Repositories;

public class NotificationRepository : INotificationRepository
{
    private readonly IMongoCollection<Notification> _collection;

    public NotificationRepository(IConfiguration configuration)
    {
        var connectionString =
            configuration["MongoDb:ConnectionString"];

        var databaseName =
            configuration["MongoDb:DatabaseName"];

        var collectionName =
            configuration["MongoDb:CollectionName"];

        var client = new MongoClient(connectionString);

        var database = client.GetDatabase(databaseName);

        _collection =
            database.GetCollection<Notification>(
                collectionName);
    }

    public async Task AddAsync(
        Notification notification,
        CancellationToken cancellationToken)
    {
        await _collection.InsertOneAsync(
            notification,
            cancellationToken: cancellationToken);
    }

    public async Task<List<Notification>> GetByUserIdAsync(
        string userId,
        CancellationToken cancellationToken)
    {
        return await _collection
            .Find(x => x.UserId == userId)
            .SortByDescending(x => x.CreatedAtUtc)
            .ToListAsync(cancellationToken);
    }
}
