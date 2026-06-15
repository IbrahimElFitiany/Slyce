using Orders.Application.Interfaces;
using Orders.Contract.DTOs;
using Orders.Contract.Interfaces;
using Orders.Domain;
using Orders.Domain.Aggregates.Order;
using Orders.Domain.Repositories;
using Shared.Domain.ValueObjects;

namespace Orders.Application.Services
{
    public sealed class OrderServices(IOrderRepository orderRepository, IUnitOfWork unitOfWork) : IOrderServices
    {
        public async Task CreateOrderAsync(OrderDTO orderDTO, CancellationToken cancellationToken = default)
        {
            var orderItemInputs = orderDTO.OrderItems.Select(oi => 
            new OrderItemCreationInput(
                oi.MealId,
                oi.SizeId,
                oi.Qty, 
                Price.EGP(oi.Price)));

            var deliveryAddress = new Address(
                orderDTO.City,
                orderDTO.Area,
                orderDTO.StreetName,
                orderDTO.StreetNumber,
                new Coordinates(orderDTO.Latitude, orderDTO.Longitude)
            );

            var order = Order.CreateFromSubscription(
                orderDTO.SubscriptionId,
                orderDTO.CustomerId,
                orderDTO.BranchId,
                orderItemInputs,
                new DeliveryTimeFrame(orderDTO.From, orderDTO.To),
                deliveryAddress);

            orderRepository.Add(order);
            await unitOfWork.SaveChangesAsync(CancellationToken.None);
        }
    }
}