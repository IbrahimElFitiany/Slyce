using Microsoft.AspNetCore.SignalR;
using Orders.Application.Interfaces;
using Orders.Domain.Enums;
using WebAPI.Hubs;

namespace Orders.Infrastructure.Notifications
{
    public class SignalROrderNotifier : IOrderNotifier
    {
        private readonly IHubContext<OrderHub> _hubContext;

        public SignalROrderNotifier(IHubContext<OrderHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task NotifyOrderStatusChanged(Guid orderId , Guid CustomerId, OrderStatus status)
        {
            await _hubContext.Clients.User(CustomerId.ToString()).SendAsync("OrderStatusUpdated", orderId, status);
        }
    }
}
