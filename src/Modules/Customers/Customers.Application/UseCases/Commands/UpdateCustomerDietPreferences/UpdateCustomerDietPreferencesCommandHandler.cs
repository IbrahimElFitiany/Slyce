using Customers.Application.Interfaces;
using Customers.Domain.Repositories;
using Food.Contracts;
using MediatR;
using Shared.Application.Exceptions;

namespace Customers.Application.UseCases.Commands.UpdateCustomerDietPreferences
{
    internal sealed class UpdateCustomerDietPreferencesCommandHandler(
        IUnitOfWork unitOfWork,
        IFoodQueryServices foodQueryServices,
        ICustomerRepository customerRepository) : IRequestHandler<UpdateCustomerDietPreferencesCommand>
    {
        public async Task Handle(UpdateCustomerDietPreferencesCommand command, CancellationToken ct)
        {
            var customer = await customerRepository.GetByIdAsync(command.CustomerId, ct)
                ?? throw new NotFoundException("customer", command.CustomerId);

            if (!await foodQueryServices.AllFoodPreferencesExistAsync(command.DietPreferenceIds, ct))
                throw new NotFoundException("One or more diet preferences were not found.");

            customer.UpdateDietPreferences(command.DietPreferenceIds);

            await unitOfWork.SaveChangesAsync(ct);
        }
    }
}
