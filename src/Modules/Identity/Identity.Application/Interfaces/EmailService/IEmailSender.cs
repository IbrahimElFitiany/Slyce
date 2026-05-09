namespace Identity.Application.Interfaces.EmailService
{
    public interface IEmailSender
    {
        /// <summary>
        /// Queues the email for background delivery
        /// </summary>
        Task SendAsync(EmailMessage message, CancellationToken cancellationToken);
    }
}