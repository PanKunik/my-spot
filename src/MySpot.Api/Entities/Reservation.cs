using MySpot.Api.Exceptions;
using MySpot.Api.ValueObjects;

namespace MySpot.Api.Entities;

public class Reservation
{
    public ReservationId Id { get; }
    public EmployeeName EmployeeName { get; private set; } = null!;
    public LicencePlate LicencePlate { get; private set; } = null!;
    public Date Date { get; private set; }

    public Reservation(ReservationId id, EmployeeName employeeName, LicencePlate licencePlate, Date date)
    {
        Id = id;
        EmployeeName = employeeName;
        LicencePlate = licencePlate;
        Date = date;
    }

    public void ChangeLicencePlate(LicencePlate licencePlate)
        => LicencePlate = licencePlate;
}