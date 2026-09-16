using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductService.Application.DTOs;
using ProductService.Application.Products.Commands.CreateProduct;
using ProductService.Application.Products.Commands.DeleteProduct;
using ProductService.Application.Products.Commands.UpdateProduct;
using ProductService.Application.Products.Queries.GetAllProducts;
using ProductService.Application.Products.Queries.GetProductById;

namespace ProductService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(
        CancellationToken cancellationToken)
    {
        var products = await sender.Send(
            new GetAllProductsQuery(),
            cancellationToken);

        return Ok(products);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(
        int id,
        CancellationToken cancellationToken)
    {
        var product = await sender.Send(
            new GetProductByIdQuery(id),
            cancellationToken);

        if (product is null)
            return NotFound();

        return Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateProductDto dto,
        CancellationToken cancellationToken)
    {
        var productId = await sender.Send(
            new CreateProductCommand(dto.Name, dto.Price, dto.Stock),
            cancellationToken);

        return CreatedAtAction(
            nameof(GetById),
            new { id = productId },
            null);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        UpdateProductDto dto,
        CancellationToken cancellationToken)
    {
        var found = await sender.Send(
            new UpdateProductCommand(id, dto.Name, dto.Price, dto.Stock),
            cancellationToken);

        if (!found)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(
        int id,
        CancellationToken cancellationToken)
    {
        var found = await sender.Send(
            new DeleteProductCommand(id),
            cancellationToken);

        if (!found)
            return NotFound();

        return NoContent();
    }
}
