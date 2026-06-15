using Customers.Contracts.Interfaces;
using MediatR;
using Menus.Contracts.Interfaces;
using Orders.Application.Interfaces;
using Orders.Application.Interfaces.Payment;
using Orders.Domain;
using Orders.Domain.Aggregates.Order;
using Orders.Domain.Enums;
using Orders.Domain.Repositories;
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
        public async Task<Guid> Handle(CheckoutCartCommand command, CancellationToken ct)
        {
            var cart = await cartRepository.GetCartByCustomerIdAsync(command.CustomerId, ct)
                ?? throw new NotFoundException("Cart for this Customer is not found");

            var pairs = cart.CartItems.Select(ci => (ci.MealId, ci.SizeId));

            var mealSizeSnapshots = await menuQueryServices.GetMealSizeSnapshotsAsync(pairs, ct);

            if (mealSizeSnapshots.Count != cart.CartItems.Count)
                throw new NotFoundException("some meals couldn't be fetched");

            var customerAddress = await customerServices.GetCustomerAddressByIdAsync(command.CustomerId, command.DeliveryAddressId, ct) 
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

            var order = Order.Create(
                command.CustomerId,
                cart.BranchId,
                orderItemCreationInputs,
                paymentMethod,
                deliveryAddress);

            orderRepository.Add(order);
            await unitOfWork.SaveChangesAsync(ct);

            if (paymentMethod is not OrderPaymentMethod.CashOnDelivery)
            {
                var paymentResult = await paymentService.ProcessAsync(new PaymentRequest(
                    order.Id,
                    order.TotalPrice.Amount,
                    order.TotalPrice.Currency, null, null, null, null), ct);

                if (paymentResult.Status is PaymentStatus.Failed)
                {
                    order.UpdatePaymentStatus(OrderPaymentStatus.Failed);
                    await unitOfWork.SaveChangesAsync(ct);
                    throw new Exception();
                }
            }

            cartRepository.Remove(cart);
            await unitOfWork.SaveChangesAsync(ct);

            return order.Id;
        }
    }
}