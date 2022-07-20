using System;
using System.Threading.Tasks;
using MySpot.Application.Commands;
using MySpot.Application.Services;
using MySpot.Core.Abstractions;
using MySpot.Core.Repositories;
using MySpot.Infrastructure.DAL.Repositories;
using MySpot.Tests.Unit.Shaerd;
using Xunit;

namespace MySpot.Tests.Unit.Services;

public class ReservationsServiceTests
{
    [Fact]
    public async Task GivenValidCommand_CreateReservation_ShouldAddReservation()
    {
        // Arrange
        var command = new CreateReservation(Guid.Parse("00000000-0000-0000-0000-000000000001"),
            Guid.NewGuid(), "Joe Doe", "XYZ1234", _clock.Current().AddDays(1));

        // Act
        await _reservationsService.CreateAsync(command);
    }

    [Fact]
    public async Task GivenInvalidParkingSpotId_CreateReservation_ShouldFail()
    {
        // Arrange
        var command = new CreateReservation(Guid.Parse("00000000-0000-0000-0000-000000000010"),
            Guid.NewGuid(), "Joe Doe", "XYZ1234", DateTime.UtcNow.AddDays(1));

        // Act
        var reservationId = await _reservationsService.CreateAsync(command);

        // Assert
        Assert.Null(reservationId);
    }

    [Fact]
    public async Task GivenReservationForAlreadyTakenDate_CreateReservation_ShouldFail()
    {
        // Arrange
        var command = new CreateReservation(Guid.Parse("00000000-0000-0000-0000-000000000005"),
            Guid.NewGuid(), "Joe Doe", "XYZ 1234", DateTime.UtcNow.AddDays(1));
        await _reservationsService.CreateAsync(command);

        // Act
        var reservationId = await _reservationsService.CreateAsync(command);

        // Assert
        Assert.Null(reservationId);
    }

    #region Arrange

    private readonly IClock _clock;
    private readonly ReservationsService _reservationsService;
    private readonly IWeeklyParkingSpotRepository _weeklyParkingSpotRepository;

    public ReservationsServiceTests()
    {
        _clock = new TestClock();
        _weeklyParkingSpotRepository = new InMemoryWeeklyParkingSpot(_clock);
        _reservationsService = new ReservationsService(_clock, _weeklyParkingSpotRepository);
    }

    #endregion
}