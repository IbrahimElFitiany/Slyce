using MediatR;

namespace Menus.Application.UseCases.Queries.GetMenuByRestaurantId
{
    public sealed record GetMenuByRestaurantIdQuery (Guid RestaurantId) : IRequest<GetMenuByRestaurantIdQueryResponse>;
}
