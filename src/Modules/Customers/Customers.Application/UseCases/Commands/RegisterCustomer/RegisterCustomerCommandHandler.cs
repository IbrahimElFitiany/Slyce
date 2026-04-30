using Customers.Application.Interfaces;
using Customers.Domain.Entities;
using Microsoft.Extensions.Logging;
using MediatR;
using Customers.Domain.ValueObjects;
using Shared.Domain.ValueObjects;


namespace Customers.Application.UseCases.Commands.RegisterCustomer
{
    public class RegisterCustomerCommandHandler : IRequestHandler<RegisterCustomerCommand,Guid>
    {
        private readonly ICustomerRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<RegisterCustomerCommandHandler> _logger;

        public RegisterCustomerCommandHandler(
            ICustomerRepository repository,
            ILogger<RegisterCustomerCommandHandler> logger,
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(RegisterCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = new Customer(
                fname: request.Fname,
                lname: request.Lname,
                email: Email.Create(request.Email),
                phoneNumber: PhoneNumber.Create(request.PhoneNumber),
                height:Height.FromCm(165),
                bday: request.BirthDay
            );

            _repository.Add(customer);
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation("Customer {CustomerId} Registered", customer.Id);

            return customer.Id;
        }
    }
}
