using FluentValidation;
using Orders.Domain.Enums;

namespace Orders.Application.UseCases.Commands.CheckoutCart
{
    public sealed class CheckoutCartCommandValidator : AbstractValidator<CheckoutCartCommand>
    {
        public CheckoutCartCommandValidator()
        {
            RuleFor(x => x.PaymentMethod)
                .Must(x => Enum.TryParse<OrderPaymentMethod>(x, ignoreCase: true, out _))
                .WithMessage("Invalid payment method.");
        }
    }
}