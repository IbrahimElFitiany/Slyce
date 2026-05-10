using MediatR;
using Customers.Application.Interfaces;
using Customers.Domain.ValueObjects;
using Shared.Application.Exceptions;

namespace Customers.Application.UseCases.Commands.UpdateWeight
{
    internal sealed class UpdateWeightCommandHandler(
        IUnitOfWork unitOfWork,
        ICustomerRepository customerRepository) : IRequestHandler<UpdateWeightCommand>
    {
        public async Task Handle(UpdateWeightCommand command, CancellationToken ct)
        {
            var customer = await customerRepository.GetByIdAsync(command.CustomerId, ct)
                ?? throw new NotFoundException("Customer", command.CustomerId);

            customer.UpdateWeight(Weight.FromKilograms(command.WeightKg));

            await unitOfWork.SaveChangesAsync(ct);
        }
    }
}