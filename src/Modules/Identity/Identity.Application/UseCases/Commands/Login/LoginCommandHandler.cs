using Identity.Application.Interfaces;
using Identity.Domain.Enums;
using Identity.Domain.Repositories;
using MediatR;
using Restaurants.Contracts.Interfaces;
using Shared.Application.Exceptions;
using Shared.Domain.ValueObjects;

namespace Identity.Application.UseCases.Commands.Login
{
    internal sealed class LoginCommandHandler(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        ITokenGenerator tokenGenerator,
        IRestaurantQueryServices restaurantQueryServices) : IRequestHandler<LoginCommand, LoginResult>
    {

        public async Task<LoginResult> Handle(LoginCommand command, CancellationToken ct)
        {
            var user = await userRepository.GetUserByEmailAsync(Email.Create(command.Email), ct) 
                ?? throw new UnauthorizedException("Invalid credentials");

            if (user.PasswordHash is null)
                throw new UnauthorizedException("Invalid credentials");

            if (!passwordHasher.Verify(command.Password, user.PasswordHash))
                throw new UnauthorizedException("Invalid credentials");

            Guid? restaurantId = null;
            if (user.UserType == UserType.RestaurantOwner)
            {
                restaurantId = await restaurantQueryServices.GetRestaurantIdByOwnerIdAsync(user.Id, ct);
            }

            var (token, expiresAt) = tokenGenerator.GenerateToken(user, restaurantId);

            return new LoginResult(token, expiresAt);
        }

    }
}