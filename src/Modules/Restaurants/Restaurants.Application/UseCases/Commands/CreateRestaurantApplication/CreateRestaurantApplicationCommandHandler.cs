using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Interfaces;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Enums;
using Shared.Domain.Exceptions;
using Shared.Domain.ValueObjects;

namespace Restaurants.Application.UseCases.Commands.CreateRestaurantApplication
{
    public sealed class CreateRestaurantApplicationCommandHandler : IRequestHandler<CreateRestaurantApplicationCommand,Guid>
    {
        private readonly IRestaurantApplicationRepository _restaurantApplicationRepository;
        private readonly ILogger<CreateRestaurantApplicationCommandHandler> _logger;

        public CreateRestaurantApplicationCommandHandler(
            IRestaurantApplicationRepository restaurantApplicationRepository,
            ILogger<CreateRestaurantApplicationCommandHandler> logger)
        {
            _restaurantApplicationRepository = restaurantApplicationRepository;
            _logger = logger;
        }

        public async Task<Guid> Handle(CreateRestaurantApplicationCommand request, CancellationToken cancellationToken)
        {

            if (await _restaurantApplicationRepository.ExistsByEmailAsync(request.CompanyEmail,cancellationToken))
            {
                _logger.LogWarning("Duplicate email attempted: {Email}", request.CompanyEmail);
                throw new DuplicateException(request.CompanyEmail);
            }

            if (await _restaurantApplicationRepository.ExistsByBrandNameAsync(request.BrandName, cancellationToken))
            {
                _logger.LogWarning("Duplicate BrandName attempted: {BrandName}", request.BrandName);
                throw new DuplicateException($"A restaurant application with brandName'{request.BrandName}' already exists.");
            }

            if (!Enum.TryParse<RestaurantType>(request.RestaurantType, true, out var restaurantType))
                throw new Exception($"Invalid restaurant type: '{request.RestaurantType}'.");

            var restaurantApplication = new RestaurantApplication(
                brandName: request.BrandName,
                ownerFirstName: request.OwnerFirstName,
                ownerLastName: request.OwnerLastName,
                companyEmail: Email.Create(request.CompanyEmail),
                ownerMobileNumber: PhoneNumber.Create(request.OwnerMobileNumber),
                companyMobileNumber: PhoneNumber.Create(request.CompanyMobileNumber),
                restaurantType: restaurantType,
                brancheCount: request.BranchCount,
                mainBranchLocation: new Address(
                    request.MainBranchAddress.City,
                    request.MainBranchAddress.Area,
                    request.MainBranchAddress.StreetName,
                    request.MainBranchAddress.StreetNumber,
                    new Coordinates(
                        request.MainBranchAddress.Latitude,
                        request.MainBranchAddress.Longitude
                        )
                    ),
                description: request.Description
            );

            await _restaurantApplicationRepository.AddAsync(restaurantApplication, cancellationToken);
            _logger.LogInformation("Restaurant application {ApplicationId} created for email {Email}", restaurantApplication.Id, restaurantApplication.CompanyEmail.Value);

            return restaurantApplication.Id;
        }
    }
}