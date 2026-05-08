using Identity.Application.Interfaces;
using MediatR;
using Shared.Application.Exceptions;
using Shared.Domain.ValueObjects;

namespace Identity.Application.UseCases.Commands.Login
{
    internal sealed class LoginCommandHandler(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        ITokenGenerator tokenGenerator) : IRequestHandler<LoginCommand, LoginResult>
    {

        public async Task<LoginResult> Handle(LoginCommand command, CancellationToken ct)
        {
            var user = await userRepository.GetUserByEmailAsync(Email.Create(command.Email), ct) 
                ?? throw new UnauthorizedException("Invalid credentials");

            if (user.PasswordHash is null)
                throw new UnauthorizedException("Invalid credentials");

            if (!passwordHasher.Verify(command.Password, user.PasswordHash))
                throw new UnauthorizedException("Invalid credentials");

            var (token, expiresAt) = tokenGenerator.GenerateToken(user);

            return new LoginResult(token, expiresAt);
        }

    }
}