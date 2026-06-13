using MediatR;

namespace Restaurants.Application.UseCases.Commands.UpdateBranchSchedule
{
    public sealed record UpdateBranchScheduleCommand(
        Guid RestaurantId,
        Guid BranchId,
        IReadOnlyList<DailyScheduleDTO> Schedule) : IRequest;

    public sealed record DailyScheduleDTO(
        DayOfWeek Day,
        TimeOnly OpenTime,
        TimeOnly CloseTime);
}
