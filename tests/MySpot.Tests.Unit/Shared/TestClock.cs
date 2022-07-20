using System;
using MySpot.Core.Abstractions;

namespace MySpot.Tests.Unit.Shaerd;

internal sealed class TestClock : IClock
{
    public DateTime Current() => new (2022, 6, 17);
}