using MediatR;
using Customers.Application.Interfaces;
using Shared.Application.Exceptions;
using Customers.Domain.ValueObjects;
using Customers.Domain.Repositories;

namespace Customers.Application.UseCases.Commands.UpdateHeight
{
    internal sealed class UpdateHeightCommandHandler(
        IUnitOfWork unitOfWork,
        ICustomerRepository customerRepository) : IRequestHandler<UpdateHeightCommand>
    {
        public async Task Handle(UpdateHeightCommand command, CancellationToken ct)
        {
            var customer = await customerRepository.GetByIdAsync(command.CustomerId, ct)
                ?? throw new NotFoundException("Customer", command.CustomerId);

            customer.UpdateHeight(Height.FromCm(command.HeightCm));

            await unitOfWork.SaveChangesAsync(ct);
        }
    }
}
