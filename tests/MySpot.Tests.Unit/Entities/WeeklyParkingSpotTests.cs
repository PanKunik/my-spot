using System;
using MySpot.Core.Entities;
using MySpot.Core.Exceptions;
using MySpot.Core.ValueObjects;
using Xunit;

namespace MySpot.Tests.Unit.Entities;

public class WeeklyParkingSpotTests
{
    [Theory]
    [InlineData("2020-02-02")]
    [InlineData("2024-02-02")]
    [InlineData("2022-06-16")]
    public void GivenInvalidDate_AddReservation_ShouldThrowException(string dateString)
    {
        var invalidDate = DateTime.Parse(dateString);

        // Arrange
        var reservation = new VehicleReservation(Guid.NewGuid(), "Joe Doe", "XYZ123", 2, new Date(invalidDate));

        // Act
        var exception = Record.Exception(() => _weeklyParkingSpot.AddReservation(reservation, _now));

        // Assert
        Assert.NotNull(exception);
        Assert.IsType<InvalidReservationDateException>(exception);
    }

    [Fact]
    public void GivenReservationForAlreadyExistingDate_AddReservation_ShouldFail()
    {
        // Arrange
        var reservationDate = _now.AddDays(1);
        var reservation = new VehicleReservation(Guid.NewGuid(), "Joe Doe", "XYZ123", 2, reservationDate);
        _weeklyParkingSpot.AddReservation(reservation, reservationDate);

        // Act
        var exception = Record.Exception(() => _weeklyParkingSpot.AddReservation(reservation, reservationDate));

        // Assert
        Assert.NotNull(exception);
        Assert.IsType<ParkingSpotAlreadyReservedException>(exception);
    }

    [Fact]
    public void GivenReservationForNotTakenDate_AddReservation_ShouldPass()
    {
        // Arrange
        var reservationDate = _now.AddDays(1);
        var reservation = new VehicleReservation(Guid.NewGuid(), "Joe Doe", "XYZ123", 2, reservationDate);

        // Act
        _weeklyParkingSpot.AddReservation(reservation, reservationDate);

        // Assert
        Assert.Single<Reservation>(_weeklyParkingSpot.Reservations);
        Assert.Contains(reservation, _weeklyParkingSpot.Reservations);
    }

    [Fact]
    public void GivenValidReservationId_RemoveReservation_ShouldPass()
    {
        // Arrange
        var reservationId = new ReservationId(Guid.NewGuid());
        var reservation = new VehicleReservation(reservationId, "Joe Doe", "ABC 1234", 2, new Date(_now.AddDays(1)));
        _weeklyParkingSpot.AddReservation(reservation, new Date(_now));

        // Act
        _weeklyParkingSpot.RemoveReservation(reservationId);

        // Assert
        Assert.Empty(_weeklyParkingSpot.Reservations);
    }

    [Fact]
    public void GivenValidReservationIdThanNotExists_RemoveReservation_ShouldPassAdnDoNothing()
    {
        // Arrange
        var reservationId = new ReservationId(Guid.NewGuid());
        var newNotExisitngReservationId = new ReservationId(Guid.NewGuid());
        var reservation = new VehicleReservation(reservationId, "Joe Doe", "ABC 1234", 2, new Date(_now.AddDays(1)));
        _weeklyParkingSpot.AddReservation(reservation, new Date(_now));

        // Act
        _weeklyParkingSpot.RemoveReservation(newNotExisitngReservationId);

        // Assert
        Assert.Single(_weeklyParkingSpot.Reservations);
    }

    #region ARRANGE

    private readonly WeeklyParkingSpot _weeklyParkingSpot;
    private readonly Date _now;

    public WeeklyParkingSpotTests()
    {
        _now = new Date(DateTime.Parse("2022-06-17"));
        _weeklyParkingSpot = WeeklyParkingSpot.Create(Guid.NewGuid(), new Week(_now), "P1");
    }

    #endregion
}