using FluentValidation;
using Customers.Domain.Enums;

namespace Customers.Application.UseCases.Commands.UpdateGender
{
    internal sealed class UpdateGenderCommandValidator : AbstractValidator<UpdateGenderCommand>
    {
        public UpdateGenderCommandValidator()
        {
            Console.WriteLine("testy---------------------------");
            RuleFor(x => x.CustomerId)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.Gender)
                .NotEmpty()
                .Must(g => Enum.TryParse<Gender>(g, true, out _))
                .WithMessage("Invalid gender value.");

        }
    }
}