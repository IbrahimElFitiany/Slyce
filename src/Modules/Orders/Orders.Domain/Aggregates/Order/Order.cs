using Orders.Domain.DomainEvents;
using Orders.Domain.Enums;
using Orders.Domain.Exceptions;
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
        public OrderPaymentStatus PaymentStatus { get; private set; } = OrderPaymentStatus.Pending;
        public OrderPaymentMethod PaymentMethod { get; private set; }
        public Address DeliveryAddress { get; private set; } = null!;
        public DateTime? EstimatedDeliveryTime { get; private set; }
        public DateTime? ActualDeliveryTime { get; private set; }
        public Guid? AssignedDriverId { get; private set; }

        private static readonly Dictionary<OrderPaymentStatus, HashSet<OrderPaymentStatus>> _validPaymentTransitions = new()
        {
            [OrderPaymentStatus.Pending] = [OrderPaymentStatus.Paid, OrderPaymentStatus.Failed],
            [OrderPaymentStatus.Paid] = [OrderPaymentStatus.Refunded],
            [OrderPaymentStatus.Failed] = [OrderPaymentStatus.Pending],
            [OrderPaymentStatus.Refunded] = [],
        };

        private Order() { }

        private Order(
            Guid customerId,
            Guid restaurantId,
            OrderPaymentMethod paymentMethod,
            Address deliveryAddress,
            IEnumerable<OrderItem> orderItems) 
        {
            ArgumentOutOfRangeException.ThrowIfEqual(customerId, Guid.Empty);
            ArgumentOutOfRangeException.ThrowIfEqual(restaurantId, Guid.Empty);

            if (!orderItems.Any())
                throw new OrderRequiresAtLeastOneItemException();

            Id = Guid.NewGuid();
            CustomerId = customerId;
            RestaurantId = restaurantId;
            PaymentMethod = paymentMethod;
            DeliveryAddress = deliveryAddress;
            _orderItems.AddRange(orderItems);
            TotalPrice = orderItems.Aggregate(
                Price.EGP(0),
                (total, item) => total + (item.UnitPrice * item.Quantity)
            );
            CreatedAt = UpdatedAt = DateTime.UtcNow;
        }

        public static Order CreateFromCart(
            Guid customerId,
            Guid restaurantId,
            IEnumerable<OrderItemCreationInput> cartItems,
            OrderPaymentMethod paymentMethod,
            Address deliveryAddress)
        {

            var orderItems = cartItems
                .Select(ci => new OrderItem(ci.MealId, ci.SizeId, ci.Quantity, ci.Price))
                .ToList();

            return new Order(customerId, restaurantId, paymentMethod, deliveryAddress, orderItems);
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

        public void UpdatePaymentStatus(OrderPaymentStatus newStatus)
        {
            if (!_validPaymentTransitions[PaymentStatus].Contains(newStatus))
                throw new InvalidPaymentStatusTransitionException(PaymentStatus, newStatus);

            PaymentStatus = newStatus;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}