using Identity.Application.Interfaces;
using Identity.Domain.Repositories;
using MediatR;
using Shared.Application.Exceptions;
using System.Security.Cryptography;
using System.Text;

namespace Identity.Application.UseCases.Commands.SetPassword
{
    internal sealed class SetPasswordCommandHandler(
        IUserRepository userRepository,
        ICacheService cacheService,
        IPasswordHasher passwordHasher,
        IUnitOfWork unitOfWork) : IRequestHandler<SetPasswordCommand>
    {
        public async Task Handle(SetPasswordCommand command, CancellationToken ct)
        {
            var tokenHash = Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(command.Token)));
            var key = $"identity:reset-password-token:{tokenHash}";

            var userId = await cacheService.GetAsync(key, ct);

            if (string.IsNullOrEmpty(userId))
                throw new Exception();

            var user = await userRepository.GetUserByIdAsync(Guid.Parse(userId), ct)
                ?? throw new NotFoundException("User", userId);

            user.UpdatePassword(passwordHasher.Hash(command.Password));

            await unitOfWork.SaveChangesAsync(ct);
            await cacheService.RemoveAsync(key, ct);
        }
    }
}
