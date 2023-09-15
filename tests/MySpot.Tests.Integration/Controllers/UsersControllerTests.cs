using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MySpot.Application.Commands;
using MySpot.Core.Entities;
using MySpot.Core.ValueObjects;
using MySpot.Infrastructure.Security;
using Shouldly;

namespace MySpot.Tests.Integration.Controllers;

public class UserControllerTests : ControllerTests, IDisposable
{
    private readonly TestDatabase _database;

    public UserControllerTests(OptionsProvider optionsProvider) : base(optionsProvider)
    {
        _database = new TestDatabase();
    }

    [Fact]
    public async Task SignUp_WhenInvokedWithValidCommand_ShouldReturn201CreatedStatusCode()
    {
        // Arrange
        await _database.Context.Database.MigrateAsync();

        var command = new SignUp(
            Guid.NewGuid(),
            "test-user@myspot.io",
            "test-user",
            "secretpassword",
            "Test User",
            "user"
        );

        // Act
        var response = await Client.PostAsJsonAsync("users", command);

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.Created);
    }

    public void Dispose()
    {
        _database.Dispose();
    }
}