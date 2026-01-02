namespace Restaurants.Contracts
{
    public interface IRestaurantServices
    {
        Task<bool> ExistsAsync(Guid restaurantId);
    }
}
