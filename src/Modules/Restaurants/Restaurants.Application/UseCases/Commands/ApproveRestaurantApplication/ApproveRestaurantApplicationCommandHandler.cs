using MediatR;
using Identity.Contract.Interfaces;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Interfaces;
using Restaurants.Domain.Entities;
using Shared.Application.Exceptions;

namespace Restaurants.Application.UseCases.Commands.ApproveRestaurantApplication
{
    internal sealed class ApproveRestaurantApplicationCommandHandler(
        IRestaurantRepository restaurantRepository,
        IRestaurantBranchRepository branchRepository,
        IRestaurantApplicationRepository applicationRepository,
        IUnitOfWork unitOfWork,
        IIdentityServices identityServices,
        ILogger<ApproveRestaurantApplicationCommandHandler> logger) : IRequestHandler<ApproveRestaurantApplicationCommand, Guid>
    {

        public async Task<Guid> Handle(ApproveRestaurantApplicationCommand command, CancellationToken ct)
        {
            //TODO User Permission

            var application = await applicationRepository.GetByIdAsync(command.ApplicationId, ct)
                ?? throw new NotFoundException(nameof(RestaurantApplication), command.ApplicationId);    

            application.Approve(command.UserId);


            // KNOWN DEFECT: orphaned owner account if SaveChangesAsync fails, ( 2 separate transactions)
            // KNOWN DEFECT: use personal email, not CompanyEmail
            await identityServices.CreateRestaurantOwner(
                application.OwnerFirstName,
                application.OwnerLastName,
                application.CompanyEmail.Value, ct);

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

             restaurantRepository.Add(restaurant);
             branchRepository.Add(mainBranch);

            await unitOfWork.SaveChangesAsync(ct);
            
            logger.LogInformation(
                "Application {ApplicationId} approved. Restaurant {RestaurantId} created with main branch {BranchId}",
                application.Id, restaurant.Id, mainBranch.Id);
            
            return restaurant.Id;
        }
    }
}