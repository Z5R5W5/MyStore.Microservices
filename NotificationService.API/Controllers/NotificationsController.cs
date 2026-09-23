using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using NotificationService.Application.Interfaces;

namespace NotificationService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotificationsController : ControllerBase
{
    private readonly INotificationRepository _repository;

    public NotificationsController(
        INotificationRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetByUserId(
        string userId,
        CancellationToken cancellationToken)
    {
        var notifications =
            await _repository.GetByUserIdAsync(
                userId,
                cancellationToken);

        return Ok(notifications);
    }
}
