namespace MySpot.Api.Models;

public class Reservation
{
    public int Id { get; set; }
    public string EmployeeName { get; set; } = null!;
    public string ParkingSpotName { get; set; } = null!;
    public string LicencePlate { get; set; } = null!;
    public DateTime Date { get; set; }
}