using Shouldly;

namespace MySpot.Tests.Integration.Controllers;

public class HomeControllerTests
{
    [Fact]
    public async Task Get_WhenInvoked_ShouldReturn200OkStatusCode()
    {
        // Arrange
        var app = new MySpotTestApp();

        // Act
        var response = await app.Client.GetAsync("/");
        var content = await response.Content.ReadAsStringAsync();

        // Assert
        content
            .ShouldBe("MySpot API [Dev v2]");
    }
}