namespace MySpot.Api.Commands;

public sealed record CreateReservation(
    Guid ParkingSpotId,
    Guid ReservationId,
    string EmployeeName,
    string LicencePlate,
    DateTime Date
);