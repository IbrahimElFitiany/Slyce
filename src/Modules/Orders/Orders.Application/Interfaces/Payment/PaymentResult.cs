namespace Orders.Application.Interfaces.Payment
{
    public sealed record PaymentResult(
        PaymentStatus Status,
        string? TransactionId,
        string? PaymentUrl,
        string? Error);
    public enum PaymentStatus
    {
        Pending,
        Succeeded,
        Failed
    }
}
