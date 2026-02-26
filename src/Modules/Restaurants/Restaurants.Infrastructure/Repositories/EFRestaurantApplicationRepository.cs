using Microsoft.EntityFrameworkCore;
using Restaurants.Application.Interfaces;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Persistence;
using Shared.Domain.ValueObjects;

namespace Restaurants.Infrastructure.Repositories
{
    public sealed class EFRestaurantApplicationRepository : IRestaurantApplicationRepository
    {
        private readonly RestaurantDbContext _dbContext
            ;
        public EFRestaurantApplicationRepository(RestaurantDbContext restaurantDbContext)
        {
            _dbContext = restaurantDbContext;
        }

        public void Add(RestaurantApplication restaurantApplication)
        {
            _dbContext.RestaurantApplications
                .Add(restaurantApplication);
        }

        public async Task<bool> ExistsByBrandNameAsync(string brandName, CancellationToken cancellationToken = default)
        {
             return await _dbContext.RestaurantApplications
                .AnyAsync(ra => ra.BrandName == brandName, cancellationToken);
        }

        public async Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            return await _dbContext.RestaurantApplications
                .AnyAsync(ra => ra.CompanyEmail == Email.Create(email), cancellationToken);
        }

        public async Task<bool> ExistsByMobileNumberAsync(string companyMobileNumber, CancellationToken cancellationToken = default)
        {
            return await _dbContext.RestaurantApplications
                .AnyAsync(ra => ra.CompanyEmail.Value == companyMobileNumber, cancellationToken);
        }

        public async Task<RestaurantApplication?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.RestaurantApplications
                .FindAsync(id, cancellationToken);
        }
    }
}
