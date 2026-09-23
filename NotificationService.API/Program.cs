using NotificationService.Application.Interfaces;
using NotificationService.Infrastructure.Messaging;
using NotificationService.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerUI();
builder.Services.AddControllers();
builder.Services.AddScoped<
    INotificationRepository,
    NotificationRepository>();
builder.Services.AddSingleton<RabbitMqConsumer>();

builder.Services.AddHostedService<
    RabbitMqConsumerHostedService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapSwaggerUI();

}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();


