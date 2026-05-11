using Customers.Application.Interfaces;
using Customers.Domain.Enums;
using MediatR;
using Shared.Application.Exceptions;

namespace Customers.Application.UseCases.Commands.UpdateActivityRate
{
    internal sealed class UpdateActivityRateCommandHandler(
        IUnitOfWork unitOfWork,
        ICustomerRepository customerRepository) : IRequestHandler<UpdateActivityRateCommand>
    {
        public async Task Handle(UpdateActivityRateCommand command, CancellationToken ct)
        {
            var customer = await customerRepository.GetByIdAsync(command.CustomerId, ct)
                ?? throw new NotFoundException("Customer", command.CustomerId);

            var t = Enum.Parse<ActivityRate>(command.ActivityRate);

            customer.UpdateActivityRate(t);

            await unitOfWork.SaveChangesAsync(ct);
        }
    }
}
