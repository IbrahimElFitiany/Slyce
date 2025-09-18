using Orders.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Application.DTOs
{
    public class UpdateOrderStatusRequest
    {
        public OrderStatus status { get; set; }
    }
}
