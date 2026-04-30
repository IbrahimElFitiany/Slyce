using MediatR;

namespace Customers.Application.UseCases.Commands.DeleteCustomer
{
    public sealed record DeleteCustomerCommand(Guid CustomerId) : IRequest;
}
