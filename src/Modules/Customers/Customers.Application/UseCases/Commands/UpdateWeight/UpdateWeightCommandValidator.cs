using FluentValidation;

namespace Customers.Application.UseCases.Commands.UpdateWeight
{
    internal sealed class UpdateWeightCommandValidator : AbstractValidator<UpdateWeightCommand>
    {
        public UpdateWeightCommandValidator()
        {
            RuleFor(x => x.CustomerId)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.WeightKg)
                .GreaterThan(0)
                .WithMessage("Weight must be greater than 0.");

        }
    }
}