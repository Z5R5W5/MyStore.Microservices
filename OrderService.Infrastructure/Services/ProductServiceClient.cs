using System;
using System.Collections.Generic;
using System.Text;

using System.Net.Http.Json;
using OrderService.Application.Interfaces;

namespace OrderService.Infrastructure.Services;

public class ProductServiceClient : IProductServiceClient
{
    private readonly HttpClient _httpClient;

    public ProductServiceClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ProductDto?> GetProductAsync(
        int productId,
        CancellationToken cancellationToken)
    {
        return await _httpClient.GetFromJsonAsync<ProductDto>(
            $"/api/Products/{productId}",
            cancellationToken);
    }
}
