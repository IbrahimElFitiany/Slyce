namespace Menus.Application.Interfaces
{
    public interface ICacheService
    {
        Task ClearAsync(string key, CancellationToken cancellationToken);
    }
}