using MySpot.Application.DTO;

namespace MySpot.Application.Secutiry;

public interface ITokenStorage
{
    void Set(JwtDto jwt);
    JwtDto Get();
}