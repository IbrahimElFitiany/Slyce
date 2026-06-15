using Customers.Application.Interfaces;
using Customers.Domain.Enums;
using Customers.Domain.Repositories;
using MediatR;
using Shared.Application.Exceptions;

namespace Customers.Application.UseCases.Commands.UpdateGender
{
    internal sealed class UpdateGenderCommandHandler(
        IUnitOfWork unitOfWork,
        ICustomerRepository customerRepository) : IRequestHandler<UpdateGenderCommand>
    {
        public async Task Handle(UpdateGenderCommand command, CancellationToken ct)
        {
            var customer = await customerRepository.GetByIdAsync(command.CustomerId, ct)
                ?? throw new NotFoundException("Customer", command.CustomerId);

            Enum.TryParse<Gender>(command.Gender, out var gender);

            customer.UpdateGender(gender);

            await unitOfWork.SaveChangesAsync(ct);
        }
    }
}