using MySpot.Core.Abstractions;
using MySpot.Core.ValueObjects;
using MySpot.Tests.Unit.Shaerd;
using Xunit;

namespace MySpot.Tests.Unit.ValueObjects;

public class DateTests
{
    [Fact]
    public void GivenValidDaysCount_AddDays_ShouldPass()
    {
        // Arrange
        var daysToAdd = 2;
        var date = new Date(_clock.Current());
        var currentDayOfMonth = date.Value.Date.Day;

        // Act
        var newDate = date.AddDays(daysToAdd);

        // Assert
        Assert.Equal(newDate.Value.DateTime.Day, currentDayOfMonth + daysToAdd);
    }

    #region Arrange

    private readonly IClock _clock;

    public DateTests()
    {
        _clock = new TestClock();
    }

    #endregion
}