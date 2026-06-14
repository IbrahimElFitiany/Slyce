using MediatR;
using Identity.Domain.DomainEvents;
using Identity.Application.Interfaces.EmailService;

namespace Identity.Application.DomainEventHandlers
{
    internal sealed class SendWelcomeEmailEventHandler(IEmailSender emailSender) : INotificationHandler<CustomerCreatedDomainEvent>
    {
        public async Task Handle(CustomerCreatedDomainEvent domainEvent, CancellationToken ct)
        {
            await emailSender.SendAsync(new EmailMessage(domainEvent.Email, "Welcome To Slyce", "Welcome To Slyce, Begin your journey toward a healthier, stronger you."), ct);
        }
    }
}