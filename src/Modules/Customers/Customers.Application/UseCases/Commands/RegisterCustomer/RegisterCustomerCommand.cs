using Customers.Application.DTOs;
using MediatR;

namespace Customers.Application.UseCases.Commands.RegisterCustomer
{
    public record RegisterCustomerCommand(CreateCustomerReqDTO dto) : IRequest;
}