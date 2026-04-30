using FluentValidation;

namespace Food.Application.UseCases.Queries
{
    public sealed class SearchFoodSummaryValidator : AbstractValidator<SearchFoodSummaryQuery>
    {
        public SearchFoodSummaryValidator()
        {
            RuleFor(x => x.Term)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.PageNumber)
                .GreaterThan(0);

            RuleFor(x => x.PageSize)
                .GreaterThan(0)
                .LessThanOrEqualTo(50);
        }
    }
}