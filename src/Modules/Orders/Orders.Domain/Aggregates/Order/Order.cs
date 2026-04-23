using Orders.Domain.DomainEvents;
using Orders.Domain.Enums;
using Shared.Domain.Common;
using Shared.Domain.ValueObjects;

namespace Orders.Domain.Aggregates.Order
{
    public sealed class Order : AggregateRoot
    {
        public Guid CustomerId { get; private init; }
        public Guid RestaurantId { get; private init; }

        private readonly List<OrderItem> _orderItems = [];
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();
        public OrderStatus Status { get; private set; } = OrderStatus.Pending;
        public Price TotalPrice { get; private set; } = null!;
        public PaymentStatus PaymentStatus { get; private set; } = PaymentStatus.Pending;
        public PaymentMethod PaymentMethod { get; private set; }
        public Guid DeliveryAddressId { get; private set; }
        public DateTime? EstimatedDeliveryTime { get; private set; }
        public DateTime? ActualDeliveryTime { get; private set; }
        public Guid? AssignedDriverId { get; private set; }

        public static Order Create(
            Guid customerId,
            Guid restaurantId,
            Price totalPrice,
            PaymentMethod paymentMethod,
            Guid deliveryAddressId)
        {
            return new Order
            {
                Id = Guid.NewGuid(),
                CustomerId = customerId,
                RestaurantId = restaurantId,
                TotalPrice = totalPrice,
            };
        }




        public void AssignDriver(Guid driverId)
        {
            AssignedDriverId = driverId;
        }

        public void UpdateStatus(OrderStatus status)
        {
            Status = status;

            if (status == OrderStatus.Delivered)
                ActualDeliveryTime = DateTime.UtcNow;

            RaiseDomainEvent(new OrderStatusChangedDomainEvent(Id, CustomerId, status.ToString()));
        }

        public void SetEstimatedDeliveryTime(DateTime estimatedTime)
        {
            EstimatedDeliveryTime = estimatedTime;
        }

        public void UpdatePaymentStatus(PaymentStatus status)
        {
            PaymentStatus = status;
        }
    }
}