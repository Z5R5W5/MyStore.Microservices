using ProductService.Domain.Entities;

namespace ProductService.Application.Interfaces;

public interface IProductRepository
{
    Task<List<Product>> GetAllAsync(
        CancellationToken cancellationToken);

    Task<Product?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken);

    Task AddAsync(
        Product product,
        CancellationToken cancellationToken);

    Task UpdateAsync(
        Product product,
        CancellationToken cancellationToken);

    Task DeleteAsync(
        Product product,
        CancellationToken cancellationToken);
}