using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Interfaces;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Enums;
using Shared.Domain.Exceptions;
using Shared.Domain.ValueObjects;

namespace Restaurants.Application.UseCases.Commands.CreateRestaurantApplication
{
    internal sealed class CreateRestaurantApplicationCommandHandler(
        IUnitOfWork unitOfWork,
        IRestaurantApplicationRepository restaurantApplicationRepository,
        ILogger<CreateRestaurantApplicationCommandHandler> logger) : IRequestHandler<CreateRestaurantApplicationCommand,Guid>
    {

        public async Task<Guid> Handle(CreateRestaurantApplicationCommand command, CancellationToken ct)
        {

            if (await restaurantApplicationRepository.ExistsByEmailAsync(command.CompanyEmail, ct))
            {
                logger.LogWarning("Duplicate email attempted: {Email}", command.CompanyEmail);
                throw new DuplicateException(command.CompanyEmail);
            }

            if (await restaurantApplicationRepository.ExistsByBrandNameAsync(command.BrandName, ct))
            {
                logger.LogWarning("Duplicate BrandName attempted: {BrandName}", command.BrandName);
                throw new DuplicateException($"A restaurant application with brandName'{command.BrandName}' already exists.");
            }

            if (!Enum.TryParse<RestaurantType>(command.RestaurantType, true, out var restaurantType))
                throw new Exception($"Invalid restaurant type: '{command.RestaurantType}'.");

            var restaurantApplication = new RestaurantApplication(
                brandName: command.BrandName,
                ownerFirstName: command.OwnerFirstName,
                ownerLastName: command.OwnerLastName,
                companyEmail: Email.Create(command.CompanyEmail),
                ownerEmail: Email.Create(command.OwnerEmail),
                ownerMobileNumber: PhoneNumber.Create(command.OwnerMobileNumber),
                companyMobileNumber: PhoneNumber.Create(command.CompanyMobileNumber),
                restaurantType: restaurantType,
                branchCount: command.BranchCount,
                mainBranchLocation: new Address(
                    command.MainBranchAddress.City,
                    command.MainBranchAddress.Area,
                    command.MainBranchAddress.StreetName,
                    command.MainBranchAddress.StreetNumber,
                    new Coordinates(
                        command.MainBranchAddress.Latitude,
                        command.MainBranchAddress.Longitude
                        )
                    ),
                description: command.Description
            );

            restaurantApplicationRepository.Add(restaurantApplication);

            await unitOfWork.SaveChangesAsync(ct);

            logger.LogInformation("Restaurant application {ApplicationId} created for email {Email}", restaurantApplication.Id, restaurantApplication.CompanyEmail.Value);

            return restaurantApplication.Id;
        }
    }
}