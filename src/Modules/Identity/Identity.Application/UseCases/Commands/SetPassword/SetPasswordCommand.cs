using MediatR;

namespace Identity.Application.UseCases.Commands.SetPassword
{
    public sealed record SetPasswordCommand(string Token, string Password) : IRequest;
}
