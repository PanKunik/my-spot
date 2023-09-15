using System.Net.Http.Headers;
using Microsoft.Extensions.Options;
using MySpot.Application.DTO;
using MySpot.Application.Secutiry;
using MySpot.Infrastructure.Auth;
using MySpot.Infrastructure.Services;

namespace MySpot.Tests.Integration.Controllers;

public abstract class ControllerTests : IClassFixture<OptionsProvider>
{
    private readonly IAuthenticator _authenticator;
    protected HttpClient Client { get; }

    protected JwtDto Authorize(Guid userId, string role)
    {
        var jwt = _authenticator.CreateToken(userId, role);
        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt.AccessToken);

        return jwt;
    }

    public ControllerTests(OptionsProvider optionsProvider)
    {
        var authOptions = optionsProvider.Get<AuthOptions>("auth");
        _authenticator = new Authenticator(
            new OptionsWrapper<AuthOptions>(authOptions),
            new Clock()
        );
        var app = new MySpotTestApp();
        Client = app.Client;
    }
}