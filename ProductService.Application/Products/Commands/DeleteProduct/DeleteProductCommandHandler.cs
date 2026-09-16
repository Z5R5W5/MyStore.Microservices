using MediatR;
using ProductService.Application.Interfaces;

namespace ProductService.Application.Products.Commands.DeleteProduct;

public class DeleteProductCommandHandler(
    IProductRepository productRepository)
    : IRequestHandler<DeleteProductCommand, bool>
{
    public async Task<bool> Handle(
        DeleteProductCommand request,
        CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (product is null)
            return false;

        await productRepository.DeleteAsync(product, cancellationToken);

        return true;
    }
}

