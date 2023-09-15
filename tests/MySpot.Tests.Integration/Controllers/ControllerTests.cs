namespace MySpot.Tests.Integration.Controllers;

public abstract class ControllerTests : IClassFixture<OptionsProvider>
{
    protected HttpClient Client { get; }

    public ControllerTests(OptionsProvider optionsProvider)
    {
        var app = new MySpotTestApp();
        Client = app.Client;
    }
}