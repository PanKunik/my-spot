using MySpot.Core.Abstractions;
using MySpot.Core.Entities;
using MySpot.Core.ValueObjects;

namespace MySpot.Core.Policies;

internal sealed class RegularEmployeeReservationPolicy : IReservationPolicy
{
    private readonly IClock _clock;
    private const int MaximumReservationsInWeekForEmployee = 2;
    private const int EarliestHourForEmployeeToReservParkingSpot = 4;

    public RegularEmployeeReservationPolicy(IClock clock)
    {
        _clock = clock;
    }

    public bool CanBeApplied(JobTitle jobTitle)
        => jobTitle == JobTitle.Employee;

    public bool CanReserve(IEnumerable<WeeklyParkingSpot> weeklyParkingSpots, EmployeeName employeeName)
    {
        var totalEmployeeReservations = weeklyParkingSpots
            .SelectMany(x => x.Reservations)
            .OfType<VehicleReservation>()
            .Count(x => x.EmployeeName == employeeName);

        return totalEmployeeReservations < MaximumReservationsInWeekForEmployee && _clock.Current().Hour > EarliestHourForEmployeeToReservParkingSpot;
    }
}