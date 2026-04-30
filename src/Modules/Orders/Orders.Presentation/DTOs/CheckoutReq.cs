namespace Orders.Presentation.DTOs
{
    public sealed record CheckoutReq(Guid DeliveryAddressId, string PaymentMethod);
}