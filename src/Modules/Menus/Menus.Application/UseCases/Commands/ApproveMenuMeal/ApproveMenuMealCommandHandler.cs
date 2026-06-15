using MediatR;
using Menus.Application.Interfaces;
using Menus.Domain.Repositories;
using Shared.Application.Exceptions;

namespace Menus.Application.UseCases.Commands.ApproveMenuMeal
{
    internal sealed class ApproveMenuMealCommandHandler(
        IMenuMealRepository mealRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<ApproveMenuMealCommand>
    {
        public async Task Handle(ApproveMenuMealCommand request, CancellationToken cancellationToken)
        {
            var meal = await mealRepository.GetByIdAsync(request.MealId, cancellationToken)
                ?? throw new NotFoundException("Meal", request.MealId);

            meal.MarkAsReviewed();

            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
