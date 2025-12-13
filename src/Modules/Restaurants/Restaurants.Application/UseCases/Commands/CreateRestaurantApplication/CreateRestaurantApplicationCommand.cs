using MediatR;
using Restaurants.Application.DTOs;

namespace Restaurants.Application.UseCases.Commands.CreateRestaurantApplication
{
    public record CreateRestaurantApplicationCommand(CreateRestaurantApplicationReqDTO ApplicationReqDTO) : IRequest<CreateRestaurantApplicationResDTO>;
}