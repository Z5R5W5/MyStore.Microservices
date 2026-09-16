using MediatR;
using ProductService.Application.Interfaces;
using ProductService.Domain.Entities;

namespace ProductService.Application.Products.Queries.GetProductById;

public class GetProductByIdQueryHandler(
    IProductRepository productRepository)
    : IRequestHandler<GetProductByIdQuery, Product?>
{
    public async Task<Product?> Handle(
        GetProductByIdQuery request,
        CancellationToken cancellationToken)
    {
        return await productRepository.GetByIdAsync(
            request.Id,
            cancellationToken);
    }
}

