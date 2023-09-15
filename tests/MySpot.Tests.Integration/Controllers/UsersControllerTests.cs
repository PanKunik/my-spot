using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MySpot.Application.Commands;
using MySpot.Application.DTO;
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

    [Fact]
    public async Task SignIn_WhenInvokedWithValidCredentials_ShouldReturn200OkStatusCodeAndJwt()
    {
        // Arrange
        var passwordManager = new PasswordManager(
            new PasswordHasher<User>()
        );
        var password = "secretpassword";
        var email = "test-user@myspot.io";
        var user = new User(
            new UserId(Guid.NewGuid()),
            new Email(email),
            new Username("test-user"),
            passwordManager.Secure(password),
            new FullName("Test User"),
            Role.User(),
            DateTime.Now
        );

        await _database.Context.Database.MigrateAsync();
        await _database.Context.Users.AddAsync(user);
        await _database.Context.SaveChangesAsync();

        var signIn = new SignIn(email, password);

        // Act
        var response = await Client.PostAsJsonAsync("users/sign-in", signIn);

        // Assert
        var jwt = await response.Content.ReadFromJsonAsync<JwtDto>();
        jwt.ShouldNotBeNull();
        jwt.AccessToken.ShouldNotBeNullOrWhiteSpace();
    }

    public void Dispose()
    {
        _database.Dispose();
    }
}