using Microsoft.EntityFrameworkCore;
using MySpot.Application.Abstractions;
using MySpot.Application.DTO;
using MySpot.Application.Queries;

namespace MySpot.Infrastructure.DAL.Handlers;

internal sealed class GetUsersQueryHandler : IQueryHandler<GetUsers, IEnumerable<UserDTO>>
{
    private readonly MySpotDbContext _dbContext;

    public GetUsersQueryHandler(MySpotDbContext dbContext)
        => _dbContext = dbContext;

    public async Task<IEnumerable<UserDTO>> HandleAsync(GetUsers query)
        => await _dbContext.Users
            .AsNoTracking()
            .Select(x => x.AsDto())
            .ToListAsync();
}