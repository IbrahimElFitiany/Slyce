using Microsoft.EntityFrameworkCore;
using Restaurants.Application.Interfaces;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Persistence;
using Shared.Domain.ValueObjects;

namespace Restaurants.Infrastructure.Repositories
{
    public sealed class EFRestaurantApplicationRepository : IRestaurantApplicationRepository
    {
        private readonly RestaurantDbContext _db;
        public EFRestaurantApplicationRepository(RestaurantDbContext restaurantDbContext)
        {
            _db = restaurantDbContext;
        }

        public async Task<RestaurantApplication> AddAsync(RestaurantApplication restaurantApplication, CancellationToken cancellationToken = default)
        {
            await _db.RestaurantApplications.AddAsync(restaurantApplication);
            await _db.SaveChangesAsync(cancellationToken);
            return restaurantApplication;
        }

        public Task<bool> ExistsByBrandName(string brandName, CancellationToken cancellationToken = default)
        {
            return _db.RestaurantApplications.AnyAsync(ra => ra.BrandName == brandName, cancellationToken);
        }

        public async Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            return await _db.RestaurantApplications.AnyAsync(ra => ra.CompanyEmail == Email.Create(email), cancellationToken);
        }

        public async Task<bool> ExistsByMobileNumberAsync(string companyMobileNumber, CancellationToken cancellationToken = default)
        {
            return await _db.RestaurantApplications.AnyAsync(ra => ra.CompanyEmail.Value == companyMobileNumber, cancellationToken);
        }

        public Task<RestaurantApplication?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<RestaurantApplication>> ListAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
