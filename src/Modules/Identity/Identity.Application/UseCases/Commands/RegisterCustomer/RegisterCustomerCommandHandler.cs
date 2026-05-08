using Customers.Contracts.Interfaces;
using Identity.Application.Interfaces;
using Identity.Domain.Aggregates;
using MediatR;
using Microsoft.Extensions.Logging;
using Shared.Domain.ValueObjects;

namespace Identity.Application.UseCases.Commands.RegisterCustomer
{
    internal sealed class RegisterCustomerCommandHandler(
        IUnitOfWork unitOfWork,
        IUserRepository userRepository,
        ICustomerServices customerServices,
        IPasswordHasher passwordHasher,
        ILogger<RegisterCustomerCommandHandler> logger) : IRequestHandler<RegisterCustomerCommand, Guid>
    {

        public async Task<Guid> Handle(RegisterCustomerCommand command, CancellationToken ct)
        {
            // KNOWN BUG: User and Customer creation are not atomic, if CreateCustomerAsync fails,
            // the User record is already committed with no rollback.
            // Will be addressed via outbox/integration events in the future
            
            // TODO: Replace with transactional outbox pattern
            var user = User.CreateCustomer(
                command.FirstName,
                command.LastName,
                Email.Create(command.Email),
                PhoneNumber.Create(command.PhoneNumber),
                passwordHasher.Hash(command.Password));

            userRepository.Add(user);
            await unitOfWork.SaveChangesAsync(ct);
            logger.LogInformation("User registered: {UserId}", user.Id);

            await customerServices.CreateCustomerAsync(user.Id, command.BirthDay, ct);

            return user.Id;
        }
    }
}