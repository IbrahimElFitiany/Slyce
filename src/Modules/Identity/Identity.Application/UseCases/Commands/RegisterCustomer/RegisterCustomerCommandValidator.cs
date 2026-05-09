using FluentValidation;

namespace Identity.Application.UseCases.Commands.RegisterCustomer
{
    internal sealed class RegisterCustomerCommandValidator : AbstractValidator<RegisterCustomerCommand>
    {
        public RegisterCustomerCommandValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.LastName)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Password)
                .NotEmpty();

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .MinimumLength(8);

            RuleFor(x => x.BirthDay)
                .LessThan(DateOnly.FromDateTime(DateTime.UtcNow));
        }
    }
}