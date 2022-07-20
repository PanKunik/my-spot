using MySpot.Core.Entities;
using MySpot.Core.ValueObjects;

namespace MySpot.Core.Policies;

internal sealed class ManagerReservationPolicy : IReservationPolicy
{
    private const int MaximumReservationsInWeekForManager = 4;

    public bool CanBeApplied(JobTitle jobTitle)
        => jobTitle == JobTitle.Manager;

    public bool CanReserve(IEnumerable<WeeklyParkingSpot> weeklyParkingSpots, EmployeeName employeeName)
    {
        var totalEmployeeReservations = weeklyParkingSpots
            .SelectMany(x => x.Reservations)
            .Count(x => x.EmployeeName == employeeName);

        return totalEmployeeReservations < MaximumReservationsInWeekForManager; // TODO: Pull it from JSON config file or from database dictionary
    }
}