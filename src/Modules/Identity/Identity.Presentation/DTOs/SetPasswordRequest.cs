namespace Identity.Presentation.DTOs
{
    public sealed record SetPasswordRequest(string Token, string Password);
}