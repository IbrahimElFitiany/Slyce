using FluentValidation;

namespace Customers.Application.UseCases.Commands.UpdateHeight
{
    internal sealed class UpdateHeightCommandValidator : AbstractValidator<UpdateHeightCommand>
    {
        public UpdateHeightCommandValidator() {

            RuleFor(x => x.HeightCm)
                .GreaterThan(0);
        }
    }
}
