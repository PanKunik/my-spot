using MySpot.Api.Models;

namespace MySpot.Api.Services;

public class ReservationsService
{
    private static string[] _parkingSpotNames = { "P1", "P2", "P3", "P4", "P5" };
    private static readonly List<Reservation> _reservation = new();

    private static int Id = 1;

    public IEnumerable<Reservation> GetAll()
        => _reservation;

    public Reservation Get(int id)
        => _reservation.SingleOrDefault(r => r.Id == id);

    public int? Create(Reservation reservation)
    {
        reservation.Id = Id;
        reservation.Date = DateTime.Now.AddDays(1).Date;

        if(_parkingSpotNames.All(ps => ps != reservation.ParkingSpotName))
        {
            return default;
        }

        if(_reservation.Any(r => r.Date.Date == reservation.Date.Date && r.ParkingSpotName == reservation.ParkingSpotName))
        {
            return default;
        }

        _reservation.Add(reservation);
        Id++;

        return reservation.Id;
    }

    public bool Update(Reservation reservation)
    {
        var existingReservation = _reservation.SingleOrDefault(r => r.Id == reservation.Id);

        if(existingReservation is null)
        {
            return false;
        }

        existingReservation.LicencePlate = reservation.LicencePlate;
        return true;
    }

    public bool Delete(int id)
    {
        var existingReservation = _reservation.SingleOrDefault(r => r.Id == id);

        if(existingReservation is null)
        {
            return false;
        }

        return _reservation.Remove(existingReservation);
    }
}