using Microsoft.Extensions.Logging;
using Orders.Contract.Interfaces;
using Orders.Contract.DTOs;
using Subscriptions.Domain.Repositories;


namespace Subscriptions.Application.Jobs
{
    public sealed class SubscriptionOrderGenerationJob(
        ISubscriptionRepository subscriptionRepo,
        IOrderServices orderService,
        ILogger<SubscriptionOrderGenerationJob> logger)
    {

        public async Task ExecuteAsync()
        {
            logger.LogInformation("Subscription order generation started at {Time}", DateTime.UtcNow);

            var activeSubscriptions = await subscriptionRepo.GetAllActiveAsync();

            foreach (var subscription in activeSubscriptions)
            {
                try
                {
                    await orderService.CreateOrderAsync(new OrderDTO(
                        CustomerId: subscription.CustomerId,
                        BranchId: subscription.BranchId,
                        SubscriptionId: subscription.Id,
                        OrderItems: subscription.SubscriptionMeals.Select( sm => new OrderItemDTO(sm.MealId, sm.SizeId, sm.Quantity, sm.PriceAtSubscription.Amount)),
                        City: subscription.DeliveryAddress.City,
                        Area: subscription.DeliveryAddress.Area,
                        StreetName: subscription.DeliveryAddress.StreetName,
                        StreetNumber: subscription.DeliveryAddress.StreetNumber,
                        Latitude: subscription.DeliveryAddress.Coordinates.Latitude,
                        Longitude:subscription.DeliveryAddress.Coordinates.Longitude,
                        From: subscription.TimeFrame.From,
                        To: subscription.TimeFrame.To));
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Failed to create order for subscription {SubscriptionId}", subscription.Id);
                }
            }

            logger.LogInformation("Subscription order generation finished. Processed {Count} subscriptions", activeSubscriptions.Count);
        }
    }
}