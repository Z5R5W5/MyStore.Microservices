using MediatR;
using ProductService.Domain.Entities;

namespace ProductService.Application.Products.Queries.GetAllProducts;

public record GetAllProductsQuery : IRequest<List<Product>>;

