using MediatR;
using ProductService.Application.Interfaces;

namespace ProductService.Application.Products.Commands.UpdateProduct;

public class UpdateProductCommandHandler(
    IProductRepository productRepository)
    : IRequestHandler<UpdateProductCommand, bool>
{
    public async Task<bool> Handle(
        UpdateProductCommand request,
        CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (product is null)
            return false;

        product.Update(request.Name, request.Price, request.Stock);

        await productRepository.UpdateAsync(product, cancellationToken);

        return true;
    }
}

