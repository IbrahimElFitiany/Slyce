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
        IReadOnlyList<T> Items)
    {
        public static PagedResult<T> Empty(int page, int pageSize) => new(0, pageSize, page, 0, false, false, []);
    }
}
