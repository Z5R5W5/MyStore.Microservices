using System;
using System.Collections.Generic;
using System.Text;

namespace OrderService.Domain.Entities;

public enum OrderStatus
{
    Pending = 1,
    Confirmed = 2,
    Cancelled = 3
}
