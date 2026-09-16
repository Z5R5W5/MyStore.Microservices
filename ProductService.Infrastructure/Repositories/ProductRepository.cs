using Microsoft.EntityFrameworkCore;
using ProductService.Application.Interfaces;
using ProductService.Domain.Entities;
using ProductService.Infrastructure.Data;

namespace ProductService.Infrastructure.Repositories;

public class ProductRepository(ProductDbContext context)
    : IProductRepository
{
    public async Task<List<Product>> GetAllAsync(
        CancellationToken cancellationToken)
    {
        return await context.Products
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Product?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken)
    {
        return await context.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(
                p => p.Id == id,
                cancellationToken);
    }

    public async Task AddAsync(
        Product product,
        CancellationToken cancellationToken)
    {
        await context.Products.AddAsync(
            product,
            cancellationToken);

        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(
        Product product,
        CancellationToken cancellationToken)
    {
        context.Products.Update(product);

        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(
        Product product,
        CancellationToken cancellationToken)
    {
        context.Products.Remove(product);

        await context.SaveChangesAsync(cancellationToken);
    }
}