using Customers.Application.Interfaces;
using Customers.Domain.Entities;
using Microsoft.Extensions.Logging;
using MediatR;
using Customers.Domain.ValueObjects;


namespace Customers.Application.UseCases.Commands.RegisterCustomer
{
    public class RegisterCustomerCommandHandler : IRequestHandler<RegisterCustomerCommand>
    {
        private readonly ICustomerRepository _repository;
        private readonly ILogger<RegisterCustomerCommandHandler> _logger;

        public RegisterCustomerCommandHandler(
            ICustomerRepository repository,
            ILogger<RegisterCustomerCommandHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task Handle(RegisterCustomerCommand request, CancellationToken cancellationToken)
        {
            var dto = request.dto;

            var customer = new Customer(dto.fname,
                dto.lname,
                Email.Create(dto.email),
                PhoneNumber.Create(dto.phoneNumber),
                Height.FromCm(165),
                dto.birthDay
            );

            await _repository.AddAsync(customer,cancellationToken);
            _logger.LogInformation("Customer {CustomerId} Registered", customer.Id);

        }
    }
}
