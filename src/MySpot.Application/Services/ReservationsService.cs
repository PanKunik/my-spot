using MySpot.Application.Commands;
using MySpot.Application.DTO;
using MySpot.Application.Exceptions;
using MySpot.Core.Entities;
using MySpot.Core.Repositories;
using MySpot.Core.ValueObjects;

namespace MySpot.Application.Services;

public class ReservationsService : IReservationsService
{
    private readonly IClock _clock;
    private readonly IWeeklyParkingSpotRepository _weeklyParkingSpotRepository;

    public ReservationsService(IClock clock, IWeeklyParkingSpotRepository weeklyParkingSpotRepository)
    {
        _clock = clock;
        _weeklyParkingSpotRepository = weeklyParkingSpotRepository;
    }

    public async Task<IEnumerable<ReservationDTO>> GetWeeklyAsync()
        => (await _weeklyParkingSpotRepository
            .GetAllAsync())
            .SelectMany(w => w.Reservations)
            .Select(x => new ReservationDTO()
            {
                Id = x.Id,
                EmployeeName = x.EmployeeName,
                LicencePlate = x.LicencePlate,
                Date = x.Date.Value.Date
            });

    public async Task<ReservationDTO> GetAsync(Guid id)
        => (await GetWeeklyAsync())
            .SingleOrDefault(w => w.Id == id);

    public async Task CreateAsync(CreateReservation command)
    {
        var (spotId, reservationId, employeeName, licencePlate, date) = command;

        var weeklyParkingSpot = await _weeklyParkingSpotRepository.GetAsync(spotId);

        if (weeklyParkingSpot is null)
        {
            throw new WeeklyParkingSpotNotFoundException();
        }

        var reservation = new Reservation(reservationId, employeeName, licencePlate, new Date(date));

        weeklyParkingSpot.AddReservation(reservation, new Date(CurrentDate()));
        await _weeklyParkingSpotRepository.UpdateAsync(weeklyParkingSpot);
    }

    public async Task UpdateAsync(ChangeReservationLicencePlate command)
    {
        var weeklyParkingSpot = await GetWeeklyParkingSpotByReservationAsync(command.ReservationId);

        if (weeklyParkingSpot is null)
        {
            throw new WeeklyParkingSpotNotFoundException();
        }

        var reservationId = new ReservationId(command.ReservationId);
        var reservation = weeklyParkingSpot.Reservations
            .SingleOrDefault(x => x.Id == reservationId);

        if (reservation is null)
        {
            throw new ReservationNotFoundException(command.ReservationId);
        }

        reservation.ChangeLicencePlate(command.LicencePlate);
        await _weeklyParkingSpotRepository.UpdateAsync(weeklyParkingSpot);
    }

    public async Task DeleteAsync(DeleteReservation command)
    {
        var weeklyParkingSpot = await GetWeeklyParkingSpotByReservationAsync(command.ReservationId);

        if (weeklyParkingSpot is null)
        {
            throw new WeeklyParkingSpotNotFoundException();
        }

        weeklyParkingSpot.RemoveReservation(command.ReservationId);
        await _weeklyParkingSpotRepository.UpdateAsync(weeklyParkingSpot);
    }

    private async Task<WeeklyParkingSpot> GetWeeklyParkingSpotByReservationAsync(ReservationId id)
        => (await _weeklyParkingSpotRepository
            .GetAllAsync())
            .SingleOrDefault(x => x.Reservations.Any(r => r.Id == id));

    private DateTime CurrentDate() => _clock.Current();
}