using MediatR;
using ProductService.Domain.Entities;

namespace ProductService.Application.Products.Queries.GetProductById;

public record GetProductByIdQuery(int Id) : IRequest<Product?>;

