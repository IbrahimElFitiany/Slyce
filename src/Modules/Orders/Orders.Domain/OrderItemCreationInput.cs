using Shared.Domain.ValueObjects;

namespace Orders.Domain
{
    public sealed record OrderItemCreationInput(Guid MealId, Guid SizeId, int Quantity, Price Price);
}
