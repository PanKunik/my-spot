using MySpot.Core.Abstractions;

namespace MySpot.Infrastructure.Services;

internal sealed class Clock : IClock
{
    public DateTime Current() => DateTime.UtcNow;
}