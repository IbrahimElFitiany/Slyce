using Microsoft.AspNetCore.SignalR;

namespace WebAPI.Hubs
{
    public class OrderHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            Console.WriteLine("User connected with ConnectionId: " + Context.ConnectionId);

            if (Context.User?.Identity?.IsAuthenticated == true)
            {
                Console.WriteLine("Claims for user:");
                foreach (var claim in Context.User.Claims)
                {
                    Console.WriteLine($"Type: {claim.Type}, Value: {claim.Value}");
                }
            }
            else
            {
                Console.WriteLine("User is not authenticated.");
            }

            return base.OnConnectedAsync();
        }
    }
}
