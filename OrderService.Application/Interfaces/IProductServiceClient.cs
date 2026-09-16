using System;
using System.Collections.Generic;
using System.Text;

namespace OrderService.Application.Interfaces;

public interface IProductServiceClient
{
    Task<ProductDto?> GetProductAsync(
        int productId,
        CancellationToken cancellationToken);
}

public record ProductDto(
    int Id,
    string Name,
    decimal Price,
    int Stock);
