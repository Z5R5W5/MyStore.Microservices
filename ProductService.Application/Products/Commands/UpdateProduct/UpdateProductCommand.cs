using MediatR;

namespace ProductService.Application.Products.Commands.UpdateProduct;

public record UpdateProductCommand(
    int Id,
    string Name,
    decimal Price,
    int Stock) : IRequest<bool>;

