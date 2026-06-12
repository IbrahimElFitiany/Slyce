using Customers.Application.UseCases.Queries.GetCustomerProfile;
using Customers.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Application.Exceptions;
using System.Linq;

namespace Customers.Infrastructure.Queries
{
    internal sealed class GetCustomerProfileQueryHandler(
        CustomersDbContext customersDbContext) : IRequestHandler<GetCustomerProfileQuery, CustomerProfileResponse>
    {
        public async Task<CustomerProfileResponse> Handle(GetCustomerProfileQuery query, CancellationToken cancellationToken)
        {
            var profile = await customersDbContext.Customers
                .AsNoTracking()
                .Where(c => c.Id == query.CustomerId)
                .Select(c => new CustomerProfileResponse(
                    c.Gender.ToString(),
                    c.Weight.Kilograms,
                    c.Height.Centimeters,
                    c.ActivityRate.ToString(),
                    c.Allergens,
                    c.DietPreferences))
                .FirstOrDefaultAsync(cancellationToken)
                ?? throw new NotFoundException("Customer", query.CustomerId);

            return profile;
        }
    }
}