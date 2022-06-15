using MySpot.Api.Commands;
using MySpot.Api.DTO;
using MySpot.Api.Entities;

namespace MySpot.Api.Services;

public class ReservationsService
{
    private static WeeklyParkingSpot[] _weeklyParkingSpots = 
    {
        new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000001"), DateTime.Now.AddDays(-2), DateTime.Now.AddDays(5), "P1"),
        new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000002"), DateTime.Now.AddDays(-2), DateTime.Now.AddDays(5), "P2"),
        new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000003"), DateTime.Now.AddDays(-2), DateTime.Now.AddDays(5), "P3"),
        new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000004"), DateTime.Now.AddDays(-2), DateTime.Now.AddDays(5), "P4"),
        new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000005"), DateTime.Now.AddDays(-2), DateTime.Now.AddDays(5), "P5"),
    };

    public IEnumerable<ReservationDTO> GetWeekly()
        => _weeklyParkingSpots
            .SelectMany(w => w.Reservations)
            .Select(x => new ReservationDTO()
            {
                Id = x.Id,
                EmployeeName = x.EmployeeName,
                Date = x.Date.Date
            });

    public ReservationDTO Get(Guid id)
        => GetWeekly()
            .SingleOrDefault(w => w.Id == id);

    public Guid? Create(CreateReservation command)
    {
        var(parkingSpotId, reservationId, employeeName, licencePlate, date) = command;

        var weeklyParkingSpot = _weeklyParkingSpots.SingleOrDefault(w => w.Id == parkingSpotId);
        
        if(weeklyParkingSpot is null)
        {
            return default;
        }

        var reservation = new Reservation(reservationId, employeeName, licencePlate, date);

        weeklyParkingSpot.AddReservation(reservation);
        return reservation.Id;
    }

    public bool Update(ChangeReservationLicencePlate command)
    {
        var weeklyParkingSpot = GetWeeklyParkingSpotByReservation(command.ReservationId);
        
        if(weeklyParkingSpot is null)
        {
            return false;
        }

        var reservation = weeklyParkingSpot.Reservations
            .SingleOrDefault(x => x.Id == command.ReservationId);

        if(reservation is null)
        {
            return false;
        }

        reservation.ChangeLicencePlate(command.LicencePlate);
        return true;
    }

    public bool Delete(DeleteReservation command)
    {
        var weeklyParkingSpot = GetWeeklyParkingSpotByReservation(command.ReservationId);

        if(weeklyParkingSpot is null)
        {
            return false;
        }

        weeklyParkingSpot.RemoveReservation(command.ReservationId);
        return true;
    }

    private WeeklyParkingSpot GetWeeklyParkingSpotByReservation(Guid id)
        => _weeklyParkingSpots
            .SingleOrDefault(x => x.Reservations.Any(r => r.Id == id));
}