using MediatR;

namespace Customers.Application.UseCases.Commands.UpdateGender
{
    public sealed record UpdateGenderCommand(Guid CustomerId, string Gender) : IRequest;
}