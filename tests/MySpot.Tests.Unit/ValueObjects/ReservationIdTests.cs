using System;
using MySpot.Core.Exceptions;
using MySpot.Core.ValueObjects;
using Xunit;

namespace MySpot.Tests.Unit.ValueObjects;

public class ReservationIdTests
{
    [Fact]
    public void GivenEmptyGuid_CreateReservationId_ShouldFail()
    {
        // Act
        var exception = Record.Exception(() => new ReservationId(Guid.Empty));

        // Assert
        Assert.NotNull(exception);
        Assert.IsType<InvalidEntityIdException>(exception);
    }

    [Theory]
    [InlineData("502d39a4-40d3-4c2b-8db8-21f9fb59027d")]
    [InlineData("be87e777-7a13-4fdf-b248-9c6a698d806a")]
    public void GivenValidStringGuid_CreateReservationId_ShouldPass(string stringGuid)
    {
        // Arrange
        var guid = Guid.Parse(stringGuid);

        // Act
        var uat = new ReservationId(guid);

        // Assert
        Assert.NotNull(uat);
        Assert.Equal(uat.Value, guid);
    }
}