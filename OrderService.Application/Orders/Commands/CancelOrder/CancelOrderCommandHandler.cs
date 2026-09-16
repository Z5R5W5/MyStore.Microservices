using MediatR;
using OrderService.Application.Interfaces;

namespace OrderService.Application.Orders.Commands.CancelOrder;

public class CancelOrderCommandHandler(
    IOrderRepository orderRepository)
    : IRequestHandler<CancelOrderCommand, bool>
{
    public async Task<bool> Handle(
        CancelOrderCommand request,
        CancellationToken cancellationToken)
    {
        var order = await orderRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (order is null)
            return false;

        order.Cancel();

        await orderRepository.UpdateAsync(order, cancellationToken);

        return true;
    }
}

