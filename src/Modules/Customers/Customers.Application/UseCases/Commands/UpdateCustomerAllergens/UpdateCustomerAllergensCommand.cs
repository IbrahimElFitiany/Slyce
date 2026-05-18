using MediatR;

namespace Customers.Application.UseCases.Commands.UpdateCustomerAllergens
{
    public sealed record UpdateCustomerAllergensCommand (Guid CustomerId, IReadOnlyCollection<Guid> AllergenIds) : IRequest;
}
