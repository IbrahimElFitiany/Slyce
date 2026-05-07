using MimeKit;
using MimeKit.Text;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;

namespace Identity.Infrastructure.EmailService
{
    internal sealed class SmtpEmailSender(IOptions<SMTPSettings> options, ILogger<SmtpEmailSender> logger)
    {
        private readonly SMTPSettings _settings = options.Value;

        public async Task SendAsync(string to, string subject, string body, CancellationToken ct)
        {
            var message = new MimeMessage();

            message.From.Add(MailboxAddress.Parse(_settings.Email));
            message.To.Add(MailboxAddress.Parse(to));
            message.Subject = subject;

            message.Body = new TextPart(TextFormat.Html)
            {
                Text = body
            };

            using var smtp = new SmtpClient();

            await smtp.ConnectAsync(
                _settings.Host,
                _settings.Port,
                MailKit.Security.SecureSocketOptions.StartTls,
                ct);

            await smtp.AuthenticateAsync(_settings.Email, _settings.Password, ct);

            await smtp.SendAsync(message, ct);
            logger.LogInformation("Email Sent to {Email}", to);

            await smtp.DisconnectAsync(true, ct);
        }
    }
}