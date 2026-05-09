namespace Identity.Application.UseCases.Commands.Login
{
    public sealed record LoginResult(string AccessToken, DateTime ExpiresAt);

}