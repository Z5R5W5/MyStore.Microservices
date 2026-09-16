using MediatR;

namespace ProductService.Application.Products.Commands.DeleteProduct;

public record DeleteProductCommand(int Id) : IRequest<bool>;

