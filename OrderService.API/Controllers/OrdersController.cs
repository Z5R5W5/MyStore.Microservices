using Microsoft.AspNetCore.Mvc;
using MediatR;
using OrderService.Application.Orders.Commands.CancelOrder;
using OrderService.Application.Orders.Commands.CreateOrder;
using OrderService.Application.Orders.Queries.GetAllOrders;
using OrderService.Application.Orders.Queries.GetOrderById;

namespace OrderService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController(ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(
        CancellationToken cancellationToken)
    {
        var orders = await sender.Send(
            new GetAllOrdersQuery(),
            cancellationToken);

        return Ok(orders);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(
        int id,
        CancellationToken cancellationToken)
    {
        var order = await sender.Send(
            new GetOrderByIdQuery(id),
            cancellationToken);

        if (order is null)
            return NotFound();

        return Ok(order);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateOrderCommand command,
        CancellationToken cancellationToken)
    {
        var orderId = await sender.Send(
            command,
            cancellationToken);

        return CreatedAtAction(
            nameof(GetById),
            new { id = orderId },
            null);
    }

    [HttpPatch("{id:int}/cancel")]
    public async Task<IActionResult> Cancel(
        int id,
        CancellationToken cancellationToken)
    {
        var found = await sender.Send(
            new CancelOrderCommand(id),
            cancellationToken);

        if (!found)
            return NotFound();

        return NoContent();
    }
}
