using MySpot.Api.Commands;
using MySpot.Api.DTO;
using MySpot.Api.Entities;
using MySpot.Api.Exceptions;
using MySpot.Api.Repositories;
using MySpot.Api.ValueObjects;

namespace MySpot.Api.Services;

public class ReservationsService : IReservationsService
{
    private readonly IClock _clock;
    private readonly IWeeklyParkingSpotRepository _weeklyParkingSpotRepository;

    public ReservationsService(IClock clock, IWeeklyParkingSpotRepository weeklyParkingSpotRepository)
    {
        _clock = clock;
        _weeklyParkingSpotRepository = weeklyParkingSpotRepository;
    }

    public IEnumerable<ReservationDTO> GetWeekly()
        => _weeklyParkingSpotRepository
            .GetAll()
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

            var weeklyParkingSpot = _weeklyParkingSpotRepository.Get(spotId);
            
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
            _weeklyParkingSpotRepository.Update(weeklyParkingSpot);

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
            _weeklyParkingSpotRepository.Update(weeklyParkingSpot);
            
            return true;
        }
        catch(CustomException ex)
        {
            return false;
        }
    }

    private WeeklyParkingSpot GetWeeklyParkingSpotByReservation(ReservationId id)
        => _weeklyParkingSpotRepository
            .GetAll()
            .SingleOrDefault(x => x.Reservations.Any(r => r.Id == id));

    private DateTime CurrentDate() => _clock.Current();
}