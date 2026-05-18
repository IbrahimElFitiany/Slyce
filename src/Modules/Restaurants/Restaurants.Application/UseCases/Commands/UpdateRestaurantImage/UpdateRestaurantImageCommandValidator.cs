using FluentValidation;

namespace Restaurants.Application.UseCases.Commands.UpdateRestaurantImage
{
    internal sealed class UpdateRestaurantImageCommandValidator : AbstractValidator<UpdateRestaurantImageCommand>
    {
        public UpdateRestaurantImageCommandValidator() {

            RuleFor(x => x.RestauarntId)
                .NotEmpty();

            RuleFor(x => x.ImageUrl)
                .NotEmpty()
                .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
                .WithMessage("ImageUrl must be a valid URL.");
        }
    }
}