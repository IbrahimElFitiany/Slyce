using Customers.Application.Interfaces;
using Customers.Domain.Repositories;
using Food.Contracts;
using MediatR;
using Shared.Application.Exceptions;

namespace Customers.Application.UseCases.Commands.UpdateCustomerAllergens
{
    internal sealed class UpdateCustomerAllergensCommandHandler(
        IUnitOfWork unitOfWork,
        IFoodQueryServices foodQueryServices,
        ICustomerRepository customerRepository) : IRequestHandler<UpdateCustomerAllergensCommand>
    {
        public async Task Handle(UpdateCustomerAllergensCommand command, CancellationToken ct)
        {
            var customer = await customerRepository.GetByIdAsync(command.CustomerId, ct)
                ?? throw new NotFoundException("customer", command.CustomerId);

            if (!await foodQueryServices.AllAllergensExistAsync(command.AllergenIds, ct))
                throw new NotFoundException("One or more allergens were not found.");

            customer.UpdateAllergens(command.AllergenIds);

            await unitOfWork.SaveChangesAsync(ct);
        }
    }
}
