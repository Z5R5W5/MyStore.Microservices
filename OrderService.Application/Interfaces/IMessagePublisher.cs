using System;
using System.Collections.Generic;
using System.Text;

namespace OrderService.Application.Interfaces;

public interface IMessagePublisher
{
    Task PublishAsync<T>(
        T message,
        string exchange,
        string routingKey,
        CancellationToken cancellationToken = default);
}
