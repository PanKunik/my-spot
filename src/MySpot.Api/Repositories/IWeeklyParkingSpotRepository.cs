using MySpot.Api.Entities;
using MySpot.Api.ValueObjects;

namespace MySpot.Api.Repositories;

public interface IWeeklyParkingSpotRepository
{
    IEnumerable<WeeklyParkingSpot> GetAll();
    WeeklyParkingSpot Get(ParkingSpotId id);
    void Add(WeeklyParkingSpot weeklyParkingSpot);
    void Update(WeeklyParkingSpot weeklyParkingSpot);
}