namespace Identity.Infrastructure.EmailService
{
    internal sealed class SMTPSettings
    {
        public string Host { get; init; }
        public int Port { get; init; }
        public string Email { get; init; }
        public string Password { get; init; }
    }
}