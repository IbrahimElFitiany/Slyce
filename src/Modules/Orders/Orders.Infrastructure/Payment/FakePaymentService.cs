using Orders.Application.Interfaces.Payment;

namespace Orders.Infrastructure.Payment
{
    internal sealed class FakePaymentService : IPaymentService
    {
        public Task<PaymentResult> ProcessAsync(PaymentRequest request, CancellationToken ct)
        {
            return Task.FromResult(new PaymentResult(PaymentStatus.Succeeded, null, null, null));
        }
    }
}
