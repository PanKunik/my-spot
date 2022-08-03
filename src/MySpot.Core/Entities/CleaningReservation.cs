using MySpot.Core.ValueObjects;

namespace MySpot.Core.Entities;

public class CleaningReservation : Reservation
{
    public CleaningReservation(ReservationId id, Date date) : base(id, 2, date)
    {
    }
}