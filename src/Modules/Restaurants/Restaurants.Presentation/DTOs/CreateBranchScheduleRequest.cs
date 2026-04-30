using System.ComponentModel.DataAnnotations;

namespace Restaurants.Presentation.DTOs
{
    public sealed record CreateBranchScheduleRequest([Required] [MinLength(1)] IReadOnlyList<DayWorkingHours> Schedule);

    public sealed record DayWorkingHours(
    DayOfWeek Day,
    TimeOnly OpeningTime,
    TimeOnly ClosingTime);
}

