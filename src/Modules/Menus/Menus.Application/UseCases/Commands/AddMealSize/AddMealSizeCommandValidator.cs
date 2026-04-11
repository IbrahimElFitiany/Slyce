using FluentValidation;

namespace Menus.Application.UseCases.Commands.AddMealSize
{
    public sealed class AddMealSizeCommandValidator : AbstractValidator<AddMealSizeCommand>
    {
        public AddMealSizeCommandValidator()
        {
            RuleFor(x => x.MealId)
                .NotEmpty();

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Price)
                .GreaterThan(0);

            RuleFor(x => x.SortOrder)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.IngredientQuantities)
                .NotEmpty();

            RuleForEach(x => x.IngredientQuantities)
                .ChildRules(iq =>
                {
                    iq.RuleFor(x => x.IngredientId).NotEmpty();
                    iq.RuleFor(x => x.Quantity).GreaterThan(0);
                });
        }

    }
}