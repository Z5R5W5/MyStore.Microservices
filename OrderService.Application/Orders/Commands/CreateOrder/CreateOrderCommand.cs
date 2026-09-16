using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace OrderService.Application.Orders.Commands.CreateOrder;

public record CreateOrderCommand(
    string UserId,
    List<CreateOrderItem> Items) : IRequest<int>;

public record CreateOrderItem(
    int ProductId,
    int Quantity);
