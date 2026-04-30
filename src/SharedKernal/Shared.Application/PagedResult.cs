namespace Shared.Application
{
    public sealed record PagedResult<T>
    (
        int Total,
        int PerPage,
        int CurrentPage,
        int TotalPages,
        bool HasNext,
        bool HasPrevious,
        IReadOnlyList<T> Items);
}
