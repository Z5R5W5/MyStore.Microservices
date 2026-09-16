using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.EntityFrameworkCore;
using OrderService.Application.Interfaces;
using OrderService.Domain.Entities;
using OrderService.Infrastructure.Data;

namespace OrderService.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly OrderDbContext _context;

    public OrderRepository(OrderDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(
        Order order,
        CancellationToken cancellationToken)
    {
        await _context.Orders.AddAsync(
            order,
            cancellationToken);

        await _context.SaveChangesAsync(
            cancellationToken);
    }

    public async Task<Order?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken)
    {
        return await _context.Orders
            .Include(o => o.Items)
            .FirstOrDefaultAsync(
                o => o.Id == id,
                cancellationToken);
    }

    public async Task<List<Order>> GetAllAsync(
        CancellationToken cancellationToken)
    {
        return await _context.Orders
            .Include(o => o.Items)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task UpdateAsync(
        Order order,
        CancellationToken cancellationToken)
    {
        _context.Orders.Update(order);

        await _context.SaveChangesAsync(
            cancellationToken);
    }
}
