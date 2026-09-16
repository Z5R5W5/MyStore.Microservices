using Microsoft.EntityFrameworkCore;
using OrderService.Application.Interfaces;
using OrderService.Application.Orders.Commands.CreateOrder;
using OrderService.Infrastructure.Data;
using OrderService.Infrastructure.Repositories;
using OrderService.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddConsole();

builder.Services.AddHttpLogging(options =>
{
    options.LoggingFields =
        Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.All;
});

builder.Services.AddOpenApi();
builder.Services.AddSwaggerUI();
builder.Services.AddControllers();

builder.Services.AddDbContext<OrderDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("OrderConnection")));

builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddHttpClient<IProductServiceClient, ProductServiceClient>(
    client =>
    {
        client.BaseAddress = new Uri(
            builder.Configuration["Services:ProductService"]!);
    });

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(
        typeof(CreateOrderCommand).Assembly);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapSwaggerUI();

}

app.UseHttpLogging();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();