using System;
using MySpot.Api.Entities;
using MySpot.Api.ValueObjects;
using Xunit;

namespace MySpot.Tests.Unit.Entities;

public class ReservationTests
{
    [Theory]
    [InlineData("ABC123")]
    [InlineData("XYZ 123")]
    public void GivenValidLicencePlate_ChangeLicencePlate_ShouldPass(string licencePlate)
    {
        // Arrange
        var reservation = new Reservation(new ReservationId(Guid.NewGuid()), "Joe Doe", "12345", new Date(DateTime.Now));

        // Act
        reservation.ChangeLicencePlate(licencePlate);

        // Assert
        Assert.Equal(reservation.LicencePlate, licencePlate);
    }
}