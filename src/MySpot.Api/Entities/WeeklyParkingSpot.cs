using MySpot.Api.Exceptions;

namespace MySpot.Api.Entities;

public class WeeklyParkingSpot
{
    public Guid Id { get; }
    public DateTime From { get; private set; }
    public DateTime To { get; private set; }
    public string Name { get; private set; }
    public IEnumerable<Reservation> Reservations => _reservations;

    private readonly HashSet<Reservation> _reservations = new();

    public WeeklyParkingSpot(Guid id, DateTime @from, DateTime to, string name)
    {
        Id = id;
        From = @from;
        To = to;
        Name = name;
    }

    public void AddReservation(Reservation reservation)
    {
        var isInvalidDate = reservation.Date.Date < From ||
                            reservation.Date.Date < DateTime.UtcNow.Date ||
                            reservation.Date.Date > To;

        if(isInvalidDate)
        {
            throw new InvalidReservationDateException(reservation.Date);
        }

        var alreadyReserved = _reservations.Any(x => x.Date.Date == reservation.Date.Date);

        if(alreadyReserved)
        {
            throw new ParkingSpotAlreadyReservedException(Name, reservation.Date);
        }
        
        _reservations.Add(reservation);
    }

    public void RemoveReservation(Guid id)
        => _reservations.RemoveWhere(x => x.Id == id);
}