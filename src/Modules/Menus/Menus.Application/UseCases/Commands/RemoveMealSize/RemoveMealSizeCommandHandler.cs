using MediatR;
using Menus.Application.Interfaces;
using Menus.Domain.Entities;
using Menus.Domain.Repositories;
using Microsoft.Extensions.Logging;
using Shared.Application.Exceptions;

namespace Menus.Application.UseCases.Commands.RemoveMealSize
{
    internal sealed class RemoveMealSizeCommandHandler : IRequestHandler<RemoveMealSizeCommand>
    {
        private readonly IMenuMealRepository _menuMealRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<RemoveMealSizeCommandHandler> _logger;

        public RemoveMealSizeCommandHandler(
            IMenuMealRepository menuMealRepository,
            ILogger<RemoveMealSizeCommandHandler> logger,
            IUnitOfWork unitOfWork)
        {
            _menuMealRepository = menuMealRepository;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(RemoveMealSizeCommand request, CancellationToken ct)
        {
            var meal = await _menuMealRepository.GetByIdAsync(request.MealId, ct)
                ?? throw new NotFoundException(nameof(MenuMeal), request.MealId);

            meal.RemoveSize(request.SizeId);
            await _unitOfWork.SaveChangesAsync(ct);

            _logger.LogInformation("MealSize:{MealSizeId} was removed from Meal: {MealId}",request.SizeId, request.MealId);
        }
    }
}
