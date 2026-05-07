using MediatR;

namespace Identity.Application.UseCases.Commands.RegisterCustomer
{
    public sealed record RegisterCustomerCommand(
        string FirstName,
        string LastName,
        string Email,
        string Password,
        string PhoneNumber,
        DateOnly BirthDay) : IRequest<Guid>;
}