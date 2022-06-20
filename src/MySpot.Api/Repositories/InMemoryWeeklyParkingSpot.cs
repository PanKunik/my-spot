using MySpot.Api.Entities;
using MySpot.Api.Services;
using MySpot.Api.ValueObjects;

namespace MySpot.Api.Repositories;

public class InMemoryWeeklyParkingSpot : IWeeklyParkingSpotRepository
{
    private readonly List<WeeklyParkingSpot> _weeklyParkingSpots;

    public InMemoryWeeklyParkingSpot(IClock clock)
    {
        _weeklyParkingSpots = new List<WeeklyParkingSpot>()
        {
            new (Guid.Parse("00000000-0000-0000-0000-000000000001"), new Week(clock.Current()), "P1"),
            new (Guid.Parse("00000000-0000-0000-0000-000000000002"), new Week(clock.Current()), "P2"),
            new (Guid.Parse("00000000-0000-0000-0000-000000000003"), new Week(clock.Current()), "P3"),
            new (Guid.Parse("00000000-0000-0000-0000-000000000004"), new Week(clock.Current()), "P4"),
            new (Guid.Parse("00000000-0000-0000-0000-000000000005"), new Week(clock.Current()), "P5"),
        };
    }

    public IEnumerable<WeeklyParkingSpot> GetAll() => _weeklyParkingSpots;
    
    public WeeklyParkingSpot Get(ParkingSpotId id) => _weeklyParkingSpots.SingleOrDefault(w => w.Id == id);
    
    public void Add(WeeklyParkingSpot weeklyParkingSpot)
    {
        _weeklyParkingSpots.Add(weeklyParkingSpot);
    }

    public void Update(WeeklyParkingSpot weeklyParkingSpot)
    {   
    }
}