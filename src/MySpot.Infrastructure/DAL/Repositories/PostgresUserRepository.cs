using Microsoft.EntityFrameworkCore;
using MySpot.Core.Entities;
using MySpot.Core.Repositories;
using MySpot.Core.ValueObjects;

namespace MySpot.Infrastructure.DAL.Repositories;

internal sealed class PostgresUserRepository : IUserRepository
{
    private readonly DbSet<User> _users;

    public PostgresUserRepository(MySpotDbContext dbContext)
    {
        this._users = dbContext.Users;
    }

    public async Task AddAsync(User user)
        => await _users.AddAsync(user);

    public Task<User> GetByEmailAsync(string email)
        => _users.SingleOrDefaultAsync(x => x.Email == email);

    public Task<User> GetByIdAsync(UserId id)
        => _users.SingleOrDefaultAsync(x => x.UserId == id);

    public Task<User> GetByUsernameAsync(string username)
        => _users.SingleOrDefaultAsync(x => x.Username == username);
}
