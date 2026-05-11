using MediatR;

namespace Customers.Application.UseCases.Commands.UpdateHeight
{
    public sealed record UpdateHeightCommand(Guid CustomerId, int HeightCm) : IRequest;
}
