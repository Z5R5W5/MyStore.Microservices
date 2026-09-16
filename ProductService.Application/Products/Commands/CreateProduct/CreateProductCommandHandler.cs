using MediatR;
using ProductService.Application.Interfaces;
using ProductService.Domain.Entities;

namespace ProductService.Application.Products.Commands.CreateProduct;

public class CreateProductCommandHandler(
    IProductRepository productRepository)
    : IRequestHandler<CreateProductCommand, int>
{
    public async Task<int> Handle(
        CreateProductCommand request,
        CancellationToken cancellationToken)
    {
        var product = new Product(
            request.Name,
            request.Price,
            request.Stock);

        await productRepository.AddAsync(product, cancellationToken);

        return product.Id;
    }
}

