using Microsoft.AspNetCore.Http;
using MySpot.Application.DTO;
using MySpot.Application.Secutiry;

namespace MySpot.Infrastructure.Auth;

internal sealed class HttpContextTokenStorage : ITokenStorage
{
    private const string TokenKey = "jwt";
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HttpContextTokenStorage(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public void Set(JwtDto jwt) => _httpContextAccessor.HttpContext?.Items.TryAdd(TokenKey, jwt);

    public JwtDto Get() => _httpContextAccessor.HttpContext.Items.TryGetValue(TokenKey, out var jwt) ? (JwtDto)jwt : null;
}