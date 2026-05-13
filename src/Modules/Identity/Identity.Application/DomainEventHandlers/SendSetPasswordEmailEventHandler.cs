using MediatR;
using Identity.Domain.DomainEvents;
using Identity.Application.Interfaces.EmailService;
using Identity.Application.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace Identity.Application.DomainEventHandlers
{
    internal sealed class SendSetPasswordEmailEventHandler(
        IEmailSender emailSender,
        ICacheService cacheService) : INotificationHandler<RestaurantOwnerCreatedDomainEvent>
    {
        public async Task Handle(RestaurantOwnerCreatedDomainEvent domainEvent, CancellationToken ct)
        {
            var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
            var hashedToken = SHA256.HashData(Encoding.UTF8.GetBytes(token));

            await cacheService.SetAsync(
                key: $"identity:reset-password-token:{Convert.ToHexString(hashedToken)}",
                value: domainEvent.UserId.ToString(),
                cancellationToken: ct);

            await emailSender.SendAsync(
                new EmailMessage(
                    domainEvent.Email, 
                    "Please set your password",
                    $"slycefront-staticfornow.com/auth/set-password?token={token}"), ct);
        }
    }
}