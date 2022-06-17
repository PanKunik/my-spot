using System;
using MySpot.Api.Commands;
using MySpot.Api.Entities;
using MySpot.Api.Services;
using MySpot.Api.ValueObjects;
using MySpot.Tests.Unit.Shaerd;
using Xunit;

namespace MySpot.Tests.Unit.Services;

public class ReservationsServiceTests
{
    [Fact]
    public void GivenValidCommand_CreateReservation_ShouldAddReservation()
    {
        // Arrange
        var command = new CreateReservation(Guid.Parse("00000000-0000-0000-0000-000000000001"),
            Guid.NewGuid(), "Joe Doe", "XYZ1234", DateTime.UtcNow.AddDays(1));

        // Act
        var reservationId = _reservationsService.Create(command);

        // Assert
        Assert.NotNull(reservationId);
        Assert.Equal(reservationId, command.ReservationId);
    }

    [Fact]
    public void GivenInvalidParkingSpotId_CreateReservation_ShouldFail()
    {
        // Arrange
        var command = new CreateReservation(Guid.Parse("00000000-0000-0000-0000-000000000010"),
            Guid.NewGuid(), "Joe Doe", "XYZ1234", DateTime.UtcNow.AddDays(1));

        // Act
        var reservationId = _reservationsService.Create(command);

        // Assert
        Assert.Null(reservationId);
    }

    [Fact]
    public void GivenReservationForAlreadyTakenDate_CreateReservation_ShouldFail()
    {
        // Arrange
        var command = new CreateReservation(Guid.Parse("00000000-0000-0000-0000-000000000005"),
            Guid.NewGuid(), "Joe Doe", "XYZ 1234", DateTime.UtcNow.AddDays(1));
        _reservationsService.Create(command);

        // Act
        var reservationId = _reservationsService.Create(command);

        // Assert
        Assert.Null(reservationId);
    }

    #region Arrange

    private readonly IClock _clock;
    private readonly ReservationsService _reservationsService;

    public ReservationsServiceTests()
    {
        _clock = new TestClock();
        var weeklyParkingSpots = new WeeklyParkingSpot[]
        {
            new (Guid.Parse("00000000-0000-0000-0000-000000000001"), new Week(_clock.Current()), "P1"),
            new (Guid.Parse("00000000-0000-0000-0000-000000000002"), new Week(_clock.Current()), "P2"),
            new (Guid.Parse("00000000-0000-0000-0000-000000000003"), new Week(_clock.Current()), "P3"),
            new (Guid.Parse("00000000-0000-0000-0000-000000000004"), new Week(_clock.Current()), "P4"),
            new (Guid.Parse("00000000-0000-0000-0000-000000000005"), new Week(_clock.Current()), "P5"),
        };
        
        _reservationsService = new ReservationsService(_clock, weeklyParkingSpots);
    }

    #endregion
}