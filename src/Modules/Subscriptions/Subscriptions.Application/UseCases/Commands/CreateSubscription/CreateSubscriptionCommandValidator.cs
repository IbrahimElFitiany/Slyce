using FluentValidation;
using Subscriptions.Domain.Enums;

namespace Subscriptions.Application.UseCases.Commands.CreateSubscription
{
    internal sealed class CreateSubscriptionCommandValidator : AbstractValidator<CreateSubscriptionCommand>
    {
        public CreateSubscriptionCommandValidator() 
        {
            RuleFor(x => x.CustomerId)
                .NotEmpty();

            RuleFor(x => x.BranchId)
                .NotEmpty();

            RuleFor(x => x.DeliveryAddressId)
                .NotEmpty();

            RuleFor(x => x.TimeSlot)
                .NotEmpty()
                .Must(v => Enum.TryParse<TimeSlot>(v, true, out _))
                .WithMessage("Invalid TimeSlot");

            RuleFor(x => x.BillingCycle)
                .NotEmpty()
                .Must(v => Enum.TryParse<BillingCycle>(v, true, out _))
                .WithMessage("Invalid BillingCycle");

            RuleFor(x => x.SubscriptionDays)
                .NotNull()
                .Must(days => days.Count() == days.ToHashSet().Count)
                .WithMessage("Subscription days must not contain duplicates.")
                .NotEmpty();

            RuleFor(x => x.StartDate)
                .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.UtcNow));

            RuleFor(x => x.SubscriptionMeals)
                .NotNull()
                .NotEmpty()
                .Must(meals => meals.Count() == meals.ToHashSet().Count)
                .WithMessage("Subscription meals must not contain duplicates.");

            RuleForEach(x => x.SubscriptionMeals)
                .SetValidator(new SubscriptionMealInputValidator());
        }
        public sealed class SubscriptionMealInputValidator : AbstractValidator<SubscriptionMealInput>
        {
            public SubscriptionMealInputValidator()
            {
                RuleFor(x => x.SizeId)
                    .NotEmpty();

                RuleFor(x => x.Quantity)
                    .GreaterThan(0);
            }
        }
    }
}