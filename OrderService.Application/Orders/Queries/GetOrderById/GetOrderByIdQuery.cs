using MediatR;
using OrderService.Domain.Entities;

namespace OrderService.Application.Orders.Queries.GetOrderById;

public record GetOrderByIdQuery(int Id) : IRequest<Order?>;

