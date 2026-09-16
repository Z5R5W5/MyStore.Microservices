using MediatR;

namespace ProductService.Application.Products.Commands.CreateProduct;

public record CreateProductCommand(
    string Name,
    decimal Price,
    int Stock) : IRequest<int>;

