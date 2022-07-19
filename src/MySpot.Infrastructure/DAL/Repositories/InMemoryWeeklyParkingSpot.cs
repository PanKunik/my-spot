using MySpot.Application.Services;
using MySpot.Core.Entities;
using MySpot.Core.Repositories;
using MySpot.Core.ValueObjects;

namespace MySpot.Infrastructure.DAL.Repositories;

internal sealed class InMemoryWeeklyParkingSpot : IWeeklyParkingSpotRepository
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

    public async Task<IEnumerable<WeeklyParkingSpot>> GetAllAsync()
    {
        await Task.CompletedTask;
        return _weeklyParkingSpots;
    }

    public async Task<WeeklyParkingSpot> GetAsync(ParkingSpotId id)
    {
        await Task.CompletedTask;
        return _weeklyParkingSpots.SingleOrDefault(w => w.Id == id);
    }

    public Task AddAsync(WeeklyParkingSpot weeklyParkingSpot)
    {
        _weeklyParkingSpots.Add(weeklyParkingSpot);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(WeeklyParkingSpot weeklyParkingSpot) => Task.CompletedTask;
}