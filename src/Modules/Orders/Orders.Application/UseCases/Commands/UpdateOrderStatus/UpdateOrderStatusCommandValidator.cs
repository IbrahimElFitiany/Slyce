using FluentValidation;
using Orders.Domain.Enums;

namespace Orders.Application.UseCases.Commands.UpdateOrderStatus
{
    internal sealed class UpdateOrderStatusCommandValidator : AbstractValidator<UpdateOrderStatusCommand>
    {
        public UpdateOrderStatusCommandValidator()
        {
            RuleFor(x => x.Status)
                .Must(x => Enum.TryParse<OrderStatus>(x, ignoreCase: true, out _))
                .WithMessage("Invalid order status");
        }

    }
}
