namespace MySpot.Application.Commands;

public sealed record CreateReservation(
    Guid ParkingSpotId,
    Guid ReservationId,
    string EmployeeName,
    string LicencePlate,
    DateTime Date
);