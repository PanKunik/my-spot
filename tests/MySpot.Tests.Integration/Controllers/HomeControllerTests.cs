using Shouldly;

namespace MySpot.Tests.Integration.Controllers;

public class HomeControllerTests : ControllerTests
{
    public HomeControllerTests(OptionsProvider optionsProvider) : base(optionsProvider)
    {
    }

    [Fact]
    public async Task Get_WhenInvoked_ShouldReturn200OkStatusCode()
    {
        // Act
        var response = await Client.GetAsync("/");
        var content = await response.Content.ReadAsStringAsync();

        // Assert
        content
            .ShouldBe("MySpot API [Integration testing]");
    }
}