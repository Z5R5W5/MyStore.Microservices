using MediatR;

namespace OrderService.Application.Orders.Commands.CancelOrder;

public record CancelOrderCommand(int Id) : IRequest<bool>;

