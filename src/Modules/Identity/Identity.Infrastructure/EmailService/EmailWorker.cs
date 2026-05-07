using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Identity.Infrastructure.EmailService
{
    internal sealed class EmailWorker(
        EmailChannel channel,
        SmtpEmailSender sender,
        ILogger<EmailWorker> logger) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            await foreach (var job in channel.Reader.ReadAllAsync(cancellationToken))
            {
                try
                {
                    await sender.SendAsync(job.To, job.Subject, job.Body, cancellationToken);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Failed to send email to {Email}", job.To);
                }
            }
        }
    }
}