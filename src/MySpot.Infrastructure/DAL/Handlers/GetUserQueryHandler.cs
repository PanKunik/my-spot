using Microsoft.EntityFrameworkCore;
using MySpot.Application.Abstractions;
using MySpot.Application.DTO;
using MySpot.Application.Queries;

namespace MySpot.Infrastructure.DAL.Handlers;

internal sealed class GetUserQueryHandler : IQueryHandler<GetUser, UserDTO>
{
    private readonly MySpotDbContext _dbContext;

    public GetUserQueryHandler(MySpotDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<UserDTO> HandleAsync(GetUser query)
    {
        return (await _dbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.UserId.Equals(query.UserId)))
            .AsDto();
    }
}