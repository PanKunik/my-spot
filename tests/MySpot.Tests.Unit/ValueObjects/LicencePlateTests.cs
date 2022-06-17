using MySpot.Api.Exceptions;
using MySpot.Api.ValueObjects;
using Xunit;

namespace MySpot.Tests.Unit.ValueObjects;

public class LicencePlateTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void GivenNullOrEmptyLicencePlate_CreateLicencePlate_ShouldFail(string invalidLicencePlate)
    {
        // Act
        var exception = Record.Exception(() => new LicencePlate(invalidLicencePlate));
        
        // Assert
        Assert.NotNull(exception);
        Assert.IsType<InvalidLicencePlateException>(exception);
    }

    [Theory]
    [InlineData("XYZ1")]
    [InlineData("XYZ 12345")]
    public void GivenLicencePlateWithInvalidLength_CreateLicencePlate_ShouldFail(string invalidLicencePlate)
    {
        // Act
        var exception = Record.Exception(() => new LicencePlate(invalidLicencePlate));

        // Arrange
        Assert.NotNull(exception);
        Assert.IsType<InvalidLicencePlateException>(exception);
    }

    [Fact]
    public void GivenValidLicencePlate_CreateLicencePlate_ShouldPass()
    {
        // Arrange
        var licencePlate = "XYZ 123";

        // Act
        var uat = new LicencePlate(licencePlate);

        // Assert
        Assert.NotNull(uat);
        Assert.Equal(uat.Value, licencePlate);
    }
}