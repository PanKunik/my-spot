using MySpot.Core.Exceptions;
using MySpot.Core.ValueObjects;
using Xunit;

namespace MySpot.Tests.Unit.ValueObjects;

public class ParkingSpotNameTests
{
    [Fact]
    public void GivenNullParkingSpotName_CreateParkingSpotName_ShouldFail()
    {
        // Act
        var exception = Record.Exception(() => new ParkingSpotName(null));

        // Assert
        Assert.NotNull(exception);
         Assert.IsType<InvalidParkingSpotNameException>(exception);
    }

    [Fact]
    public void GivenValidParkingSpotName_CreateParkingSpotName_ShouldPass()
    {
        // Arrange
        var parkingSpotName = "P0";

        // Act
        var uat = new ParkingSpotName(parkingSpotName);

        // Assert
        Assert.NotNull(uat);
        Assert.Equal(uat.Value, parkingSpotName);
    }
}