namespace Orders.Application.Interfaces.Payment
{
    public sealed record PaymentRequest(
        Guid OrderId,
        decimal Amount,
        string Currency,
        string? CustomerEmail,
        string? CustomerName,
        string? CallbackUrl,
        string? ReturnUrl);
}