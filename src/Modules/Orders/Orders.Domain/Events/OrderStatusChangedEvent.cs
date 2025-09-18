using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Domain.Events
{
    public class OrderStatusChangedEvent
    {
        public Guid OrderId { get; }
        public Guid CustomerId { get; }
        public string Status { get; }

        public OrderStatusChangedEvent(Guid orderId, Guid customerId, string status)
        {
            OrderId = orderId;
            CustomerId = customerId;
            Status = status;
        }
    }
}
