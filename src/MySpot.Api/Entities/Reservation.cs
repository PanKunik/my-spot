namespace MySpot.Api.Entities;

public class Reservation
{
    public Guid Id { get; }
    public string EmployeeName { get; private set; } = null!;
    public string LicencePlate { get; private set; } = null!;
    public DateTime Date { get; private set; }

    public Reservation(Guid id, string employeeName, string licencePlate, DateTime date)
    {
        Id = id;
        EmployeeName = employeeName;
        LicencePlate = licencePlate;
        Date = date;
    }

    public void ChangeLicencePlate(string licencePlate)
        => LicencePlate = licencePlate;
}