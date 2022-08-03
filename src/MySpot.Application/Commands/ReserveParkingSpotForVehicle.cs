namespace MySpot.Application.Commands;

public sealed record ReserveParkingSpotForVehicle(
    Guid ParkingSpotId,
    Guid ReservationId,
    string EmployeeName,
    string LicencePlate,
    int Capacity,
    DateTime Date
);