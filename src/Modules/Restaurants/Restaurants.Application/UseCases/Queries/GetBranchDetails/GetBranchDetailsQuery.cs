using MediatR;

namespace Restaurants.Application.UseCases.Queries.GetBranchDetails
{
    public sealed record GetBranchDetailsQuery(Guid BranchId) : IRequest<GetBranchDetailsResult>;

    public sealed record GetBranchDetailsResult(
        string City,
        string Area,
        string PhoneNumber,
        IReadOnlyList<BranchWorkingHoursResult> WorkingHours);
    public sealed record BranchWorkingHoursResult(
        DayOfWeek Day,
        TimeOnly? OpenTime,
        TimeOnly? CloseTime);
}