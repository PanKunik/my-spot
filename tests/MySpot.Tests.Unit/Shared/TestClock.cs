using System;
using MySpot.Api.Services;

namespace MySpot.Tests.Unit.Shaerd;

internal sealed class TestClock : IClock
{
    public DateTime Current() => new DateTime(2022, 6, 17);
}