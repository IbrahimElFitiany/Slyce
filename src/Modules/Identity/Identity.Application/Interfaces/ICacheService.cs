namespace Identity.Application.Interfaces
{
    public interface ICacheService
    {
        Task<string?> GetAsync(string key, CancellationToken cancellationToken);
        Task SetAsync(string key, string value, CancellationToken cancellationToken);
        Task RemoveAsync(string key, CancellationToken cancellationToken);

    }
}
