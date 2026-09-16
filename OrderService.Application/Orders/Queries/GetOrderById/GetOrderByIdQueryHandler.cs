using MediatR;
using OrderService.Application.Interfaces;
using OrderService.Domain.Entities;

namespace OrderService.Application.Orders.Queries.GetOrderById;

public class GetOrderByIdQueryHandler(
    IOrderRepository orderRepository)
    : IRequestHandler<GetOrderByIdQuery, Order?>
{
    public async Task<Order?> Handle(
        GetOrderByIdQuery request,
        CancellationToken cancellationToken)
    {
        return await orderRepository.GetByIdAsync(
            request.Id,
            cancellationToken);
    }
}

