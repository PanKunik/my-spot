using MySpot.Application.Abstractions;

namespace MySpot.Application.Commands;

public sealed record ReserveParkingSpotForCleaning(DateTime Date) : ICommand;