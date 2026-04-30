using MediatR;

namespace Customers.Application.UseCases.Commands.RegisterCustomer
{
    public sealed record RegisterCustomerCommand(
        string Fname,
        string Lname,
        string Email,
        string PhoneNumber,
        DateOnly BirthDay) : IRequest<Guid>;
}