using System;
using System.Collections.Generic;
using System.Text;


namespace NotificationService.Application.Events;

    public record OrderCreatedEvent(
        int OrderId,
        string UserId);

