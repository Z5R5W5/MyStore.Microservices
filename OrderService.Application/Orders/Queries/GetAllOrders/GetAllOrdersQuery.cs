using MediatR;
using OrderService.Domain.Entities;

namespace OrderService.Application.Orders.Queries.GetAllOrders;

public record GetAllOrdersQuery : IRequest<List<Order>>;

