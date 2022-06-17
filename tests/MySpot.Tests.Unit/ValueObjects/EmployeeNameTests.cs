using MySpot.Api.Exceptions;
using MySpot.Api.ValueObjects;
using Xunit;

namespace MySpot.Tests.Unit.ValueObjects;

public class EmployeeNameTests
{
    [Theory]
    [InlineData(null)]
    public void GivenNullEployeeName_CreateEmployeeName_ShouldFail(string employeeName)
    {
        // Act
        var exception = Record.Exception(() => new EmployeeName(employeeName));
        
        // Assert
        Assert.NotNull(exception);
        Assert.IsType<InvalidEmployeeNameException>(exception);
    }

    [Theory]
    [InlineData("")]
    [InlineData("  ")]
    [InlineData("XYZ")]
    public void GivenValidEmployeeName_CreateEmployeeName_ShouldPass(string employeeName)
    {
        // Act
        var uat = new EmployeeName(employeeName);
        
        // Assert
        Assert.NotNull(uat);
        Assert.Equal(uat.Value, employeeName);
    }
}