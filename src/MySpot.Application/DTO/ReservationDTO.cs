namespace MySpot.Application.DTO;

public class ReservationDTO
{
    public Guid Id { get; set; }
    public string EmployeeName { get; set; } = null!;
    public DateTime Date { get; set; }
}