namespace MySpot.Api.DTO;

public class ReservationDTO
{
    public Guid Id { get; set; }
    public string EmployeeName { get; set; } = null!;
    public string LicencePlate { get; set; } = null!;
    public DateTime Date { get; set; }
}