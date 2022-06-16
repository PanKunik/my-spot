namespace MySpot.Api.Services;

public sealed class Clock
{
    public DateTime Current() => DateTime.UtcNow;
}