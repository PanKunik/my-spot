using MySpot.Api.Commands;
using MySpot.Api.DTO;
using MySpot.Api.Entities;
using MySpot.Api.Exceptions;
using MySpot.Api.ValueObjects;

namespace MySpot.Api.Services;

public class ReservationsService
{
    private readonly IClock _clock;
    private readonly IEnumerable<WeeklyParkingSpot> _weeklyParkingSpots;

    public ReservationsService(IClock clock, IEnumerable<WeeklyParkingSpot> weeklyParkingSpots)
    {
        _clock = clock;
        _weeklyParkingSpots = weeklyParkingSpots;
    }

    public IEnumerable<ReservationDTO> GetWeekly()
        => _weeklyParkingSpots
            .SelectMany(w => w.Reservations)
            .Select(x => new ReservationDTO()
            {
                Id = x.Id,
                EmployeeName = x.EmployeeName,
                Date = x.Date.Value.Date
            });

    public ReservationDTO Get(Guid id)
        => GetWeekly()
            .SingleOrDefault(w => w.Id == id);

    public Guid? Create(CreateReservation command)
    {
        try
        {
            var(spotId, reservationId, employeeName, licencePlate, date) = command;

            var parkingSpotId = new ParkingSpotId(spotId);
            var weeklyParkingSpot = _weeklyParkingSpots.SingleOrDefault(w => w.Id == parkingSpotId);
            
            if(weeklyParkingSpot is null)
            {
                return default;
            }

            var reservation = new Reservation(reservationId, employeeName, licencePlate, new Date(date));

            weeklyParkingSpot.AddReservation(reservation, new Date(CurrentDate()));
            return reservation.Id;
        }
        catch(CustomException ex)
        {
            return default;
        }
    }

    public bool Update(ChangeReservationLicencePlate command)
    {
        try
        {
            var weeklyParkingSpot = GetWeeklyParkingSpotByReservation(command.ReservationId);
            
            if(weeklyParkingSpot is null)
            {
                return false;
            }

            var reservationId = new ReservationId(command.ReservationId);
            var reservation = weeklyParkingSpot.Reservations
                .SingleOrDefault(x => x.Id == reservationId);

            if(reservation is null)
            {
                return false;
            }

            reservation.ChangeLicencePlate(command.LicencePlate);
            return true;
        }
        catch(CustomException ex)
        {
            return false;
        }
    }

    public bool Delete(DeleteReservation command)
    {
        try
        { 
            var weeklyParkingSpot = GetWeeklyParkingSpotByReservation(command.ReservationId);

            if(weeklyParkingSpot is null)
            {
                return false;
            }

            weeklyParkingSpot.RemoveReservation(command.ReservationId);
            return true;
        }
        catch(CustomException ex)
        {
            return false;
        }
    }

    private WeeklyParkingSpot GetWeeklyParkingSpotByReservation(ReservationId id)
        => _weeklyParkingSpots
            .SingleOrDefault(x => x.Reservations.Any(r => r.Id == id));

    private DateTime CurrentDate() => _clock.Current();
}