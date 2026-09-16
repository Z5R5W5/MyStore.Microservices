namespace ProductService.Application.DTOs;

public record CreateProductDto(
    string Name,
    decimal Price,
    int Stock);

