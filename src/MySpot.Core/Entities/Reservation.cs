using MySpot.Core.ValueObjects;

namespace MySpot.Core.Entities;

public abstract class Reservation
{
    public ReservationId Id { get; }
    public Date Date { get; private set; }

    protected Reservation(ReservationId id, Date date)
    {
        Id = id;
        Date = date;
    }
}