using FluentValidation;

namespace Menus.Application.UseCases.Commands.CreateMenuMeal
{
    internal sealed class CreateMenuMealCommandValidator : AbstractValidator<CreateMenuMealCommand>
    {
        public CreateMenuMealCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.RestaurantId)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.CategoryId)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(500);

            RuleFor(x => x.ImgUrl)
                .NotEmpty()
                .Must(BeAValidUrl)
                .WithMessage("Invalid image URL.");

            RuleFor(x => x.Ingredients)
                .NotEmpty();

            RuleFor(x => x.Sizes)
                .NotEmpty();

            RuleForEach(x => x.Sizes)
                .SetValidator(new MealSizeInputValidator());
        }

        private bool BeAValidUrl(string url) => Uri.TryCreate(url, UriKind.Absolute, out _);
    }

    internal sealed class MealSizeInputValidator : AbstractValidator<MealSizeInput>
    {
        public MealSizeInputValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.Price)
                .GreaterThan(0);

            RuleFor(x => x.SortOrder)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.IngredientQuantities)
                .NotEmpty();

            RuleForEach(x => x.IngredientQuantities)
                .SetValidator(new IngredientQuantityValidator());
        }
    }

    internal sealed class IngredientQuantityValidator : AbstractValidator<IngredientQuantityInput>
    {
        public IngredientQuantityValidator()
        {
            RuleFor(x => x.IngredientId)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.Quantity)
                .GreaterThan(0);
        }
    }
}