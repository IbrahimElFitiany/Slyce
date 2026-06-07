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
        public Guid BranchId { get; private init; }
        public Guid? SubscriptionId { get; private init; }

        private readonly List<OrderItem> _orderItems = [];
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();
        public OrderStatus Status { get; private set; } = OrderStatus.Pending;
        public Price TotalPrice { get; private set; } = null!;
        public OrderPaymentStatus PaymentStatus { get; private set; } = OrderPaymentStatus.Pending;
        public OrderPaymentMethod PaymentMethod { get; private set; }
        public Address DeliveryAddress { get; private set; } = null!;
        public DeliveryTimeFrame? DeliveryTimeFrame { get; private set; }
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

        private static readonly Dictionary<OrderStatus, HashSet<OrderStatus>> _validStatusTransitions = new()
        {
            [OrderStatus.Pending] = [OrderStatus.Cancelled, OrderStatus.Preparing],
            [OrderStatus.Preparing] = [OrderStatus.OutForDelivery],
            [OrderStatus.OutForDelivery] = [OrderStatus.Delivered],
            [OrderStatus.Delivered] = []
        };

        private Order() { }

        private Order(
            Guid customerId,
            Guid branchId,
            OrderPaymentMethod paymentMethod,
            Address deliveryAddress,
            IEnumerable<OrderItem> orderItems) 
        {
            ArgumentOutOfRangeException.ThrowIfEqual(customerId, Guid.Empty);
            ArgumentOutOfRangeException.ThrowIfEqual(branchId, Guid.Empty);

            if (!orderItems.Any())
                throw new OrderRequiresAtLeastOneItemException();

            Id = Guid.NewGuid();
            CustomerId = customerId;
            BranchId = branchId;
            PaymentMethod = paymentMethod;
            DeliveryAddress = deliveryAddress;
            _orderItems.AddRange(orderItems);
            TotalPrice = orderItems.Aggregate(
                Price.EGP(0),
                (total, item) => total + (item.UnitPrice * item.Quantity)
            );
            CreatedAt = UpdatedAt = DateTime.UtcNow;
        }

        public static Order Create(
            Guid customerId,
            Guid branchId,
            IEnumerable<OrderItemCreationInput> orderItemInputs,
            OrderPaymentMethod paymentMethod,
            Address deliveryAddress)
        {

            var orderItems = orderItemInputs
                .Select(ci => new OrderItem(ci.MealId, ci.SizeId, ci.Quantity, ci.Price));

            return new Order(customerId, branchId, paymentMethod, deliveryAddress, orderItems);
        }
        public static Order CreateFromSubscription(
            Guid subscriptionId,
            Guid customerId,
            Guid branchId,
            IEnumerable<OrderItemCreationInput> orderItemInputs,
            DeliveryTimeFrame deliveryTimeFrame,
            Address deliveryAddress)
        {

            var orderItems = orderItemInputs
                .Select(ci => new OrderItem(ci.MealId, ci.SizeId, ci.Quantity, ci.Price));

            var order = new Order(customerId, branchId, OrderPaymentMethod.Card, deliveryAddress, orderItems)
            {
                SubscriptionId = subscriptionId,
                PaymentStatus = OrderPaymentStatus.Paid,
                DeliveryTimeFrame = deliveryTimeFrame
            };

            return order;
        }

        public void AssignDriver(Guid driverId)
        {
            AssignedDriverId = driverId;
        }

        public void UpdateStatus(OrderStatus newOrderStatus)
        {
            if (!_validStatusTransitions[Status].Contains(newOrderStatus))
                throw new InvalidOrderStatusTransitionException(Status, newOrderStatus);

            Status = newOrderStatus;
            UpdatedAt = DateTime.UtcNow;

            if (newOrderStatus == OrderStatus.Delivered)
                ActualDeliveryTime = DateTime.UtcNow;

            RaiseDomainEvent(new OrderStatusChangedDomainEvent(Id, CustomerId, newOrderStatus.ToString()));
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