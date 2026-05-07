namespace Identity.Application.Interfaces.EmailService
{
    public sealed record EmailMessage(string To, string Subject, string Body);
}