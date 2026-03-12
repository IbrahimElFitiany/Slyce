using Customers.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using Shared.Application.Exceptions;

namespace Customers.Application.UseCases.Commands.DeleteCustomer
{
    public sealed class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DeleteCustomerCommandHandler> _logger;
        private readonly ICustomerRepository _customerRepository;

        public DeleteCustomerCommandHandler(
            ICustomerRepository customerRepository,
            IUnitOfWork unitOfWork,
            ILogger<DeleteCustomerCommandHandler> logger)
        {
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetByIdAsync(request.CustomerId);

            if (customer == null) 
                throw new NotFoundException(nameof(customer),request.CustomerId);

            _customerRepository.Delete(customer);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Customer {CustomerId} deleted successfully", customer.Id);
        }
    }
}