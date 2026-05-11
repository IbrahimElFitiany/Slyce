using Customers.Domain.Enums;
using FluentValidation;

namespace Customers.Application.UseCases.Commands.UpdateActivityRate
{
    internal sealed class UpdateActivityRateCommandValidator : AbstractValidator<UpdateActivityRateCommand>
    {
        public UpdateActivityRateCommandValidator() {

            RuleFor(x => x.CustomerId)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.ActivityRate)
                .Must(x => Enum.TryParse<ActivityRate>(x, true, out _))
                .WithMessage("Invalid activity rate.");

        }
    }
}
