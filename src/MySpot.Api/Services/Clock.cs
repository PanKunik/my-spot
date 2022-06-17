namespace MySpot.Api.Services;

public sealed class Clock : IClock
{
    public DateTime Current() => DateTime.UtcNow;
}