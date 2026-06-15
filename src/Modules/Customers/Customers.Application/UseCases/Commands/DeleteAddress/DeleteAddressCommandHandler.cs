using Customers.Application.Interfaces;
using Customers.Domain.Repositories;
using MediatR;
using Shared.Application.Exceptions;

namespace Customers.Application.UseCases.Commands.DeleteAddress
{
    internal sealed class DeleteAddressCommandHandler(
        ICustomerRepository customerRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<DeleteAddressCommand>
    {
        public async Task Handle(DeleteAddressCommand command, CancellationToken ct)
        {
            var customer = await customerRepository.GetByIdAsync(command.CustomerId, ct)
                ?? throw new NotFoundException("customer", command.CustomerId);

            customer.RemoveAddress(command.AddressId);

            await unitOfWork.SaveChangesAsync(ct);
        }
    }
}