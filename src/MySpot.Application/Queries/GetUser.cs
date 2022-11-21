using MySpot.Application.Abstractions;
using MySpot.Application.DTO;

namespace MySpot.Application.Queries;

public class GetUser : IQuery<UserDTO>
{
    public Guid UserId { get; set; }
}