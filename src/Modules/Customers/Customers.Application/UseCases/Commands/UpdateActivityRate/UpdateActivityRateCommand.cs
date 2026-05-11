using MediatR;

namespace Customers.Application.UseCases.Commands.UpdateActivityRate
{
    public sealed record UpdateActivityRateCommand(Guid CustomerId, string ActivityRate) : IRequest;
}