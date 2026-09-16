using MediatR;
using ProductService.Application.Interfaces;
using ProductService.Domain.Entities;

namespace ProductService.Application.Products.Queries.GetAllProducts;

public class GetAllProductsQueryHandler(
    IProductRepository productRepository)
    : IRequestHandler<GetAllProductsQuery, List<Product>>
{
    public async Task<List<Product>> Handle(
        GetAllProductsQuery request,
        CancellationToken cancellationToken)
    {
        return await productRepository.GetAllAsync(cancellationToken);
    }
}

