using MySpot.Core.Abstractions;
using MySpot.Core.ValueObjects;
using MySpot.Tests.Unit.Shaerd;
using Xunit;

namespace MySpot.Tests.Unit.ValueObjects;

public class WeekTests
{
    [Fact]
    public void GivenValidDateOffset_CreateWeek_ShouldPass()
    {
        // Arrange
        var dayOfWeek = _clock.Current().Date.DayOfWeek;
        var pastDays = -1 * (int)dayOfWeek;
        var remainingDays = 7 + pastDays;
        var properFromDate = new Date(_clock.Current().Date.AddDays(pastDays));
        var properToDate = new Date(_clock.Current().Date.AddDays(remainingDays));

        // Act
        var week = new Week(_clock.Current());

        // Assert
        Assert.NotNull(week);
        Assert.Equal(week.From, properFromDate);
        Assert.Equal(week.To, properToDate);
    }

    #region Arrange

    private readonly IClock _clock;
    
    public WeekTests()
    {
        _clock = new TestClock();
    }

    #endregion
}