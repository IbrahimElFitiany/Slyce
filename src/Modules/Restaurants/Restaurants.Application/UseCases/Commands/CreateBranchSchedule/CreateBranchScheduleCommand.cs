using MediatR;

namespace Restaurants.Application.UseCases.Commands.CreateBranchSchedule
{
    public sealed record CreateBranchScheduleCommand(
        Guid BranchId,
        IReadOnlyList<DayWorkingHoursInput> Schedule):IRequest;

    public sealed record DayWorkingHoursInput(
        DayOfWeek Day,
        TimeOnly OpeningTime,
        TimeOnly ClosingTime);
}
