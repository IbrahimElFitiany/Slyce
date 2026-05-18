using FluentValidation;
using Subscriptions.Domain.Enums;

namespace Subscriptions.Application.UseCases.Queries.GetCustomerSubscriptionsSummary
{
    internal sealed class GetCustomerSubscriptionsSummaryQueryValidator : AbstractValidator<GetCustomerSubscriptionsSummaryQuery>
    {
        public GetCustomerSubscriptionsSummaryQueryValidator(){

            RuleFor(x => x.Status)
                .Must(BeValidStatus)
                .WithMessage("Invalid status");
        }
        private bool BeValidStatus(string? status)
        {
            if (status is null)
                return true;

            return Enum.TryParse<SubscriptionStatus>(status, true, out _);
        }
    }
}