using Identity.Application.Interfaces;
using Identity.Contract.Interfaces;
using Identity.Domain.Aggregates;
using Identity.Domain.Repositories;
using Shared.Domain.ValueObjects;

namespace Identity.Application.Services
{
    public sealed class IdentityServices(
        IUserRepository userRepository,
        IUnitOfWork unitOfWork) : IIdentityServices
    {

        public async Task<Guid> CreateRestaurantOwner(string fname, string lname, string email, CancellationToken cancellationToken)
        {
            var restaurantOwner =  User.CreateRestaurantOwner(fname, lname, Email.Create(email));

            userRepository.Add(restaurantOwner);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return restaurantOwner.Id;
        }
    }
}