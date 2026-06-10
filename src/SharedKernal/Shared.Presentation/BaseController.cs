using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;


namespace Shared.Presentation
{
    public abstract class BaseController : ControllerBase
    {
        protected Guid UserId => Guid.TryParse(User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value, out var userId) ?
            userId : throw new InvalidOperationException("Invalid or missing sub claim.");

        protected Guid RestaurantId => Guid.TryParse(User.FindFirst("restaurant_id")?.Value, out var restaurantId) ?
            restaurantId : throw new InvalidOperationException("Invalid or missing sub claim.");
    }
}
