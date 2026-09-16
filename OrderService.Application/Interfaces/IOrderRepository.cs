using System;
using System.Collections.Generic;
using System.Text;

using OrderService.Domain.Entities;

namespace OrderService.Application.Interfaces;

public interface IOrderRepository
{
    Task AddAsync(
        Order order,
        CancellationToken cancellationToken);

    Task<Order?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken);

    Task<List<Order>> GetAllAsync(
        CancellationToken cancellationToken);

    Task UpdateAsync(
        Order order,
        CancellationToken cancellationToken);
}