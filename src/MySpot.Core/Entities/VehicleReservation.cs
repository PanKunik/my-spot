using MySpot.Core.ValueObjects;

namespace MySpot.Core.Entities;

public class VehicleReservation : Reservation
{
    public EmployeeName EmployeeName { get; private set; }
    public LicencePlate LicencePlate { get; private set; }

    public VehicleReservation(ReservationId id, EmployeeName employeeName, LicencePlate licencePlate, Date date)
        : base(id, date)
    {
        EmployeeName = employeeName;
        LicencePlate = licencePlate;
    }

    public void ChangeLicencePlate(LicencePlate licencePlate)
        => LicencePlate = licencePlate;
}