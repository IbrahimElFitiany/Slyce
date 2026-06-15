using MediatR;
using Customers.Contracts.Interfaces;
using Menus.Contracts.Interfaces;
using Microsoft.Extensions.Logging;
using Restaurants.Contracts.DTOs;
using Restaurants.Contracts.Interfaces;
using Shared.Application.Exceptions;
using Shared.Domain.ValueObjects;
using Subscriptions.Application.Interfaces;
using Subscriptions.Domain.Entities;
using Subscriptions.Domain.Enums;
using Subscriptions.Domain.Services;
using Subscriptions.Domain.ValueObjects;
using Menus.Contracts.DTOs;
using Subscriptions.Domain.Repositories;

namespace Subscriptions.Application.UseCases.Commands.CreateSubscription
{
    internal sealed class CreateSubscriptionCommandHandler(
        ICustomerServices customerServices,
        IRestaurantQueryServices restaurantServices,
        IMenuQueryServices menuQueryServices,
        IUnitOfWork unitOfWork,
        ISubscriptionRepository subscriptionRepository,
        SubscriptionEligibilityService eligibilityService,
        ILogger<CreateSubscriptionCommandHandler> logger) : IRequestHandler<CreateSubscriptionCommand, Guid>
    {

        public async Task<Guid> Handle(CreateSubscriptionCommand command, CancellationToken ct)
        {
            var branch = await GetBranchForSubscriptionAsync(command.BranchId, ct);
            var customerAddress = await customerServices.GetCustomerAddressByIdAsync(command.CustomerId, command.DeliveryAddressId, ct);
            var meals = await GetMealSizesAsync(command, branch.RestaurantId, ct);

            var deliveryTimeFrame = Enum.Parse<TimeSlot>(command.TimeSlot).ToDeliveryTimeFrame();
            var branchSchedule = new BranchSchedule(
                branch.Schedule.ToDictionary(
                    kvp => kvp.Key,
                    kvp => new TimeRange(kvp.Value.OpeningHour, kvp.Value.ClosingHour)
                )
            );

            eligibilityService.EnsureScheduleMatches(deliveryTimeFrame, command.SubscriptionDays, branchSchedule);
            await eligibilityService.EnsureCustomerIsWithinRadius(
               customerCoordinates: new Coordinates(customerAddress.Latitude, customerAddress.Longitude),
               branchCoordinates: new Coordinates(branch.Latitude, branch.Longitude));

            var subscription = new Subscription(
                customerId: command.CustomerId,
                branchId: command.BranchId,
                deliveryAddressId: command.DeliveryAddressId,
                deliveryAddress: new Address(
                    customerAddress.City,
                    customerAddress.Area,
                    customerAddress.StreetName,
                    customerAddress.StreetNumber,
                    new Coordinates(
                        customerAddress.Latitude,
                        customerAddress.Longitude)),
                deliveryDays: command.SubscriptionDays.Select(d => new SubscriptionDeliveryDay(d)),
                timeFrame: deliveryTimeFrame,
                subscriptionMeals: command.SubscriptionMeals.Select(submeal => {
                    var meal = meals.First(m => m.MealSizeId == submeal.SizeId);
                    return new SubscriptionMeal(
                        mealId: meal.MealId,
                        sizeId: meal.MealSizeId,
                        quantity: submeal.Quantity,
                        priceAtSubscription: Price.EGP(meal.Price)); //TODO figure out how to handle multiple currency 
                }),
                billingCycle: Enum.Parse<BillingCycle>(command.BillingCycle),
                startDate: command.StartDate);

            subscriptionRepository.Add(subscription);
            await unitOfWork.SaveChangesAsync(ct);

            logger.LogInformation("Subscription {SubscriptionId} created for Customer {CustomerId} at Branch {BranchId} starting {StartDate}.",
                subscription.Id,
                command.CustomerId,
                command.BranchId,
                command.StartDate);

            return subscription.Id;
        }

        private async Task<BranchForSubscription> GetBranchForSubscriptionAsync(Guid branchId, CancellationToken ct)
        {
            var branch = await restaurantServices.GetBranchForSubscriptionAsync(branchId, ct)
                ?? throw new NotFoundException("branch", branchId);

            return branch;
        }

        private async Task<IReadOnlyCollection<MealSizeDTO>> GetMealSizesAsync(CreateSubscriptionCommand command, Guid restaurantId, CancellationToken ct)
        {
            var sizeIds = command.SubscriptionMeals.Select(sm => sm.SizeId).Distinct().ToList();
            var sizes = await menuQueryServices.GetMealSizesByRestaurantAsync(sizeIds, restaurantId, ct);

            if (sizes.Count != sizeIds.Count)
                throw new NotFoundException("One or more meal sizes do not exist or do not belong to the restaurant");

            return sizes;
        }
    }
}