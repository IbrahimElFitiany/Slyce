using FluentValidation;
using Restaurants.Domain.Enums;

namespace Restaurants.Application.UseCases.Queries.GetRestaurantApplicationsSummary
{
    internal sealed class GetRestaurantApplicationsSummaryQueryValidator : AbstractValidator<GetRestaurantApplicationsSummaryQuery>
    {
        public GetRestaurantApplicationsSummaryQueryValidator()
        {
            RuleFor(x => x.Page)
                .GreaterThan(0);

            RuleFor(x => x.PageSize)
                .InclusiveBetween(1, 50);

            RuleFor(x => x.Status)
                .Must(BeValidStatus)
                .WithMessage("Invalid Restaurant Application Status");

            RuleFor(x => x.To)
                .GreaterThanOrEqualTo(x => x.From)
                .When(x => x.From is not null && x.To is not null)
                .WithMessage("To must be greater than or equal to From");
        }

        private static bool BeValidStatus(string? status)
        {
            if (string.IsNullOrWhiteSpace(status))
                return true;

            return Enum.TryParse<ApplicationStatus>(status, true, out _);
        }
    }
}