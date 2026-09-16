namespace ProductService.Application.DTOs;

public record UpdateProductDto(
    string Name,
    decimal Price,
    int Stock);

