using Orders.Domain.Enums;
using Orders.Domain.Events;

namespace Orders.Domain.Entities
{
    public class Order
    {
        public Guid Id { get; private set; }
        public Guid CustomerId { get; private set; }
        public Guid RestaurantId { get; private set; }

        public OrderStatus Status { get; private set; } = OrderStatus.Pending;

        public decimal TotalPrice { get; private set; }

        public PaymentStatus PaymentStatus { get; private set; } = PaymentStatus.Pending;
        public PaymentMethod PaymentMethod { get; private set; }

        public DateTime OrderDate { get; private set; } = DateTime.UtcNow;

        public Guid DeliveryAddressId { get; private set; }

        public DateTime? EstimatedDeliveryTime { get; private set; }
        public DateTime? ActualDeliveryTime { get; private set; }

        public Guid? AssignedDriverId { get; private set; }

        private readonly List<object> _domainEvents = new();
        public IReadOnlyCollection<object> DomainEvents => _domainEvents.AsReadOnly();

        public Order(Guid customerId, Guid restaurantId, decimal totalPrice, PaymentMethod paymentMethod, Guid deliveryAddressId)
        {
            Id = Guid.NewGuid();
            CustomerId = customerId;
            RestaurantId = restaurantId;
            TotalPrice = totalPrice;
            PaymentMethod = paymentMethod;
            DeliveryAddressId = deliveryAddressId;
        }

        public void AssignDriver(Guid driverId)
        {
            AssignedDriverId = driverId;
        }

        public void UpdateStatus(OrderStatus status)
        {
            Status = status;
            if (status == OrderStatus.Delivered)
            {
                ActualDeliveryTime = DateTime.UtcNow;
            }
            _domainEvents.Add(new OrderStatusChangedEvent(Id, CustomerId, status.ToString()));
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
