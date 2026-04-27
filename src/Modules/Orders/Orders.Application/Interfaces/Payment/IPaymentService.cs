namespace Orders.Application.Interfaces.Payment
{
    public interface IPaymentService
    {
        Task<PaymentResult> ProcessAsync(PaymentRequest request, CancellationToken cancellationToken);
    }
}
