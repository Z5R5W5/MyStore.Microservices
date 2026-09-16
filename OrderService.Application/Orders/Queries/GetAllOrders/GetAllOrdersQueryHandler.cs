using MediatR;
using OrderService.Application.Interfaces;
using OrderService.Domain.Entities;

namespace OrderService.Application.Orders.Queries.GetAllOrders;

public class GetAllOrdersQueryHandler(
    IOrderRepository orderRepository)
    : IRequestHandler<GetAllOrdersQuery, List<Order>>
{
    public async Task<List<Order>> Handle(
        GetAllOrdersQuery request,
        CancellationToken cancellationToken)
    {
        return await orderRepository.GetAllAsync(cancellationToken);
    }
}

