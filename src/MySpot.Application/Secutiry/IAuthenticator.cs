using MySpot.Application.DTO;

namespace MySpot.Application.Secutiry;

public interface IAuthenticator
{
    JwtDto CreateToken(Guid userId, string role);
}