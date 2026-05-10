using MediatR;

namespace Customers.Application.UseCases.Commands.UpdateWeight
{
    public sealed record UpdateWeightCommand(Guid CustomerId, decimal WeightKg) : IRequest;
}