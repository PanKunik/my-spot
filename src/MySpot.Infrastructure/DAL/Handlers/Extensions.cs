using MySpot.Application.DTO;
using MySpot.Core.Entities;

namespace MySpot.Infrastructure.DAL.Handlers;

public static class Extensions
{
    public static WeeklyParkingSpotDTO AsDto(this WeeklyParkingSpot entity)
        => new()
        {
            Id = entity.Id.Value.ToString(),
            Name = entity.Name,
            Capacity = entity.Capacity,
            From = entity.Week.From.Value.DateTime,
            To = entity.Week.To.Value.DateTime,
            Reservations = entity.Reservations.Select(x => new ReservationDTO
            {
                Id = x.Id,
                EmployeeName = x is VehicleReservation vr ? vr.EmployeeName : null,
                Date = x.Date.Value.Date
            })
        };

    public static UserDTO AsDto(this User entity)
        => new()
        {
            Id = entity.UserId,
            Username = entity.Username,
            FullName = entity.FullName
        };
}