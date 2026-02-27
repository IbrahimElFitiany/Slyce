using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Interfaces;
using Restaurants.Domain.Entities;
using Shared.Application.Exceptions;

namespace Restaurants.Application.UseCases.Commands.ApproveRestaurantApplication
{
    public class ApproveRestaurantApplicationCommandHandler : IRequestHandler<ApproveRestaurantApplicationCommand, Guid>
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IRestaurantBranchRepository _branchRepository;
        private readonly IRestaurantApplicationRepository _applicationRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ApproveRestaurantApplicationCommandHandler> _logger;

        public ApproveRestaurantApplicationCommandHandler(
            IRestaurantRepository restaurantRepository,
            IRestaurantBranchRepository branchRepository,
            IRestaurantApplicationRepository applicationRepository,
            IUnitOfWork unitOfWork,
            ILogger<ApproveRestaurantApplicationCommandHandler> logger) 
        {
            _restaurantRepository = restaurantRepository;
            _applicationRepository = applicationRepository;
            _branchRepository = branchRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Guid> Handle(ApproveRestaurantApplicationCommand request, CancellationToken cancellationToken)
        {
            //TODO User Permission
            //TODO Command Validation

            var application = await _applicationRepository.GetByIdAsync(request.ApplicationId, cancellationToken);

            if (application is null)
                throw new NotFoundException(nameof(RestaurantApplication), request.ApplicationId);

            application.Approve(request.UserId);

            var restaurant = new Restaurant(
                brandName: application.BrandName,
                image: null,
                description: null,
                restaurantType: application.RestaurantType);

            var mainBranch = new RestaurantBranch(
                restaurant.Id,
                null,
                application.MainBranchLocation,
                application.CompanyMobileNumber);

             _restaurantRepository.Add(restaurant);
             _branchRepository.Add(mainBranch);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            _logger.LogInformation(
                "Application {ApplicationId} approved. Restaurant {RestaurantId} created with main branch {BranchId}",
                application.Id, restaurant.Id, mainBranch.Id);
            
            return restaurant.Id;
        }
    }
}