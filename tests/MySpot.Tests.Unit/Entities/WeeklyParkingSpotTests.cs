using System;
using MySpot.Api.Entities;
using MySpot.Api.Exceptions;
using MySpot.Api.ValueObjects;
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
        var reservation = new Reservation(Guid.NewGuid(), "Joe Doe", "XYZ123", new Date(invalidDate));
        
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
        var reservation = new Reservation(Guid.NewGuid(), "Joe Doe", "XYZ123", reservationDate);
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
        var reservation = new Reservation(Guid.NewGuid(), "Joe Doe", "XYZ123", reservationDate);

        // Act
        _weeklyParkingSpot.AddReservation(reservation, reservationDate);

        // Assert
        Assert.Single<Reservation>(_weeklyParkingSpot.Reservations);
        Assert.Contains(reservation, _weeklyParkingSpot.Reservations);
    }

    #region ARRANGE

    private readonly WeeklyParkingSpot _weeklyParkingSpot;
    private readonly Date _now;

    public WeeklyParkingSpotTests()
    {
        _now = new Date(DateTime.Parse("2022-06-17"));
        _weeklyParkingSpot = new WeeklyParkingSpot(Guid.NewGuid(), new Week(_now), "P1");
    }

    #endregion
}