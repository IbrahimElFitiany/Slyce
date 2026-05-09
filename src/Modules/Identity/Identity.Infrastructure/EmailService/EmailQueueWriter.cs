using Identity.Application.Interfaces.EmailService;

namespace Identity.Infrastructure.EmailService
{
    internal sealed class EmailQueueWriter(EmailChannel emailChannel) : IEmailSender
    {
        public async Task SendAsync(EmailMessage job, CancellationToken ct)
        {
            await emailChannel.Writer.WriteAsync(job, ct);
        }
    }
}