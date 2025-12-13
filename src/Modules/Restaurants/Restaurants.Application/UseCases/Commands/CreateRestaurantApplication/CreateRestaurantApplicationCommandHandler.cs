using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.DTOs;
using Restaurants.Application.Interfaces;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.UseCases.Commands.CreateRestaurantApplication
{
    public sealed class CreateRestaurantApplicationCommandHandler : IRequestHandler<CreateRestaurantApplicationCommand,CreateRestaurantApplicationResDTO>
    {
        private readonly IRestaurantApplicationRepository _restaurantApplicationRepository;
        private readonly ILogger<CreateRestaurantApplicationCommandHandler> _logger;

        public CreateRestaurantApplicationCommandHandler(
            IRestaurantApplicationRepository repository,
            ILogger<CreateRestaurantApplicationCommandHandler> logger)
        {
            _restaurantApplicationRepository = repository;
            _logger = logger;
        }

        public async Task<CreateRestaurantApplicationResDTO> Handle(CreateRestaurantApplicationCommand request, CancellationToken cancellationToken)
        {
            var dto = request.ApplicationReqDTO;

            if (await _restaurantApplicationRepository.ExistsByEmailAsync(dto.CompanyEmail))
            {
                _logger.LogWarning("Duplicate email attempted: {Email}", dto.CompanyEmail);
                throw new DuplicateEmailException(dto.CompanyEmail);
            }

            var application = new RestaurantApplication(
                dto.BrandName,
                dto.OwnerFirstName,
                dto.OwnerLastName,
                dto.CompanyEmail,
                dto.MobileNumber,
                dto.RestaurantType,
                dto.Branches,
                dto.Description
            );

            await _restaurantApplicationRepository.AddAsync(application, cancellationToken);

            _logger.LogInformation("Restaurant application {ApplicationId} created for email {Email}",application.Id, dto.CompanyEmail);

            return new CreateRestaurantApplicationResDTO(application.Id);
        }
    }
}