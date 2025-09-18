using Orders.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Application.Interfaces
{
    public interface IOrderNotifier
    {
        Task NotifyOrderStatusChanged(Guid orderId,Guid CustomerId, OrderStatus orderStatus);
    }
}
