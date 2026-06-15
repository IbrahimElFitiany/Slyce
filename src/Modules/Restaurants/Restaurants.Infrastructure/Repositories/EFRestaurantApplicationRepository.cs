using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;
using Shared.Domain.ValueObjects;

namespace Restaurants.Infrastructure.Repositories
{
    public sealed class EFRestaurantApplicationRepository (RestaurantDbContext restaurantDbContext) : IRestaurantApplicationRepository
    {
        public void Add(RestaurantApplication restaurantApplication) => restaurantDbContext.RestaurantApplications.Add(restaurantApplication);

        public async Task<bool> ExistsByBrandNameAsync(string brandName, CancellationToken cancellationToken = default)
        {
             return await restaurantDbContext.RestaurantApplications
                .AnyAsync(ra => ra.BrandName == brandName, cancellationToken);
        }

        public async Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            return await restaurantDbContext.RestaurantApplications
                .AnyAsync(ra => ra.CompanyEmail == Email.Create(email), cancellationToken);
        }

        public async Task<bool> ExistsByMobileNumberAsync(string companyMobileNumber, CancellationToken cancellationToken = default)
        {
            return await restaurantDbContext.RestaurantApplications
                .AnyAsync(ra => ra.CompanyEmail.Value == companyMobileNumber, cancellationToken);
        }

        public async Task<RestaurantApplication?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await restaurantDbContext.RestaurantApplications
                .FindAsync(id, cancellationToken);
        }
    }
}