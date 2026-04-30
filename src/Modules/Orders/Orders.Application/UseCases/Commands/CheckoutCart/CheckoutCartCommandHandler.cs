using Customers.Contracts.Interfaces;
using MediatR;
using Menus.Contracts.Interfaces;
using Orders.Application.Interfaces;
using Orders.Application.Interfaces.Payment;
using Orders.Domain;
using Orders.Domain.Aggregates.Order;
using Orders.Domain.Enums;
using Shared.Application.Exceptions;
using Shared.Domain.ValueObjects;

namespace Orders.Application.UseCases.Commands.CheckoutCart
{
    internal sealed class CheckoutCartCommandHandler(
        IUnitOfWork unitOfWork,
        IPaymentService paymentService,
        ICartRepository cartRepository,
        IOrderRepository orderRepository,
        ICustomerServices customerServices,
        IMenuQueryServices menuQueryServices) : IRequestHandler<CheckoutCartCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IPaymentService _paymentService = paymentService;
        private readonly ICartRepository _cartRepository = cartRepository;
        private readonly IOrderRepository _orderRepository = orderRepository;
        private readonly ICustomerServices _customerServices = customerServices;
        private readonly IMenuQueryServices _menuQueryServices = menuQueryServices;

        public async Task<Guid> Handle(CheckoutCartCommand command, CancellationToken ct)
        {
            var cart = await _cartRepository.GetCartByCustomerIdAsync(command.CustomerId, ct)
                ?? throw new NotFoundException("Cart for this Customer is not found");

            var pairs = cart.CartItems.Select(ci => (ci.MealId, ci.SizeId));

            var mealSizeSnapshots = await _menuQueryServices.GetMealSizeSnapshotsAsync(pairs, ct);

            if (mealSizeSnapshots.Count != cart.CartItems.Count)
                throw new NotFoundException("some meals couldn't be fetched");

            var customerAddress = await _customerServices.GetCustomerAddressByIdAsync(command.CustomerId, command.DeliveryAddressId, ct) 
                ?? throw new NotFoundException("Customer Address", command.DeliveryAddressId);

            var deliveryAddress = new Address(
                customerAddress.City,
                customerAddress.Area,
                customerAddress.StreetName,
                customerAddress.StreetNumber,
                new Coordinates(customerAddress.Latitude, customerAddress.Longitude)); 


            var orderItemCreationInputs = cart.CartItems
                .Select(
                    ci => new OrderItemCreationInput(
                    ci.MealId,
                    ci.SizeId,
                    ci.Quantity,
                    Price.EGP(mealSizeSnapshots[(ci.MealId, ci.SizeId)].Price)));

            var paymentMethod = Enum.Parse<OrderPaymentMethod>(command.PaymentMethod, ignoreCase: true);

            var order = Order.CreateFromCart(
                command.CustomerId,
                cart.RestaurantId,
                orderItemCreationInputs,
                paymentMethod,
                deliveryAddress);

            _orderRepository.Add(order);
            await _unitOfWork.SaveChangesAsync(ct);

            if (paymentMethod is not OrderPaymentMethod.CashOnDelivery)
            {
                var paymentResult = await _paymentService.ProcessAsync(new PaymentRequest(
                    order.Id,
                    order.TotalPrice.Amount,
                    order.TotalPrice.Currency, null, null, null, null), ct);

                if (paymentResult.Status is PaymentStatus.Failed)
                {
                    order.UpdatePaymentStatus(OrderPaymentStatus.Failed);
                    await _unitOfWork.SaveChangesAsync(ct);
                    throw new Exception();
                }
            }

            _cartRepository.Remove(cart);
            await _unitOfWork.SaveChangesAsync(ct);

            return order.Id;
        }
    }
}