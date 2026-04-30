using Shared.Domain.ValueObjects;

namespace Subscriptions.Domain.Interfaces
{
    public interface IDistanceCalculator
    {
        Task<bool> LocationWithinLocationRadius(Coordinates x , Coordinates y);
    }
}