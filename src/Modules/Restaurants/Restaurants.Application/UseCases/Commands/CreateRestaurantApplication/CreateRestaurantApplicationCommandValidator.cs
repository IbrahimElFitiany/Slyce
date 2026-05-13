using FluentValidation;
using Restaurants.Domain.Enums;

namespace Restaurants.Application.UseCases.Commands.CreateRestaurantApplication
{
    internal sealed class CreateRestaurantApplicationCommandValidator : AbstractValidator<CreateRestaurantApplicationCommand>
    {
        public CreateRestaurantApplicationCommandValidator()
        {
            RuleFor(x => x.BrandName)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.OwnerFirstName)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.OwnerLastName)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.CompanyEmail)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(255);

            RuleFor(x => x.OwnerEmail)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(255);

            RuleFor(x => x.OwnerMobileNumber)
                .NotEmpty();

            RuleFor(x => x.CompanyMobileNumber)
                .NotEmpty();

            RuleFor(x => x.RestaurantType)
                .Must(x => Enum.TryParse<RestaurantType>(x, true, out _))
                .WithMessage("Invalid restaurant type.");

            RuleFor(x => x.BranchCount)
                .GreaterThan(0);

            RuleFor(x => x.Description)
                .MaximumLength(1000)
                .When(x => x.Description is not null);

            RuleFor(x => x.MainBranchAddress)
                .NotNull();

            RuleFor(x => x.MainBranchAddress)
                .SetValidator(new MainBranchAddressInputValidator());
        }
    }

    internal sealed class MainBranchAddressInputValidator : AbstractValidator<MainBranchAddressInput>
    {
        public MainBranchAddressInputValidator()
        {
            RuleFor(x => x.City)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Area)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.StreetName)
                .MaximumLength(100)
                .When(x => x.StreetName is not null);

            RuleFor(x => x.StreetNumber)
                .MaximumLength(20)
                .When(x => x.StreetNumber is not null);

            RuleFor(x => x.Latitude)
                .InclusiveBetween(-90, 90);

            RuleFor(x => x.Longitude)
                .InclusiveBetween(-180, 180);
        }
    }


}