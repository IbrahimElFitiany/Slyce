using MediatR;

namespace Customers.Application.UseCases.Commands.UpdateCustomerDietPreferences
{
    public sealed record UpdateCustomerDietPreferencesCommand(Guid CustomerId, IReadOnlyCollection<Guid> DietPreferenceIds) : IRequest;
}
