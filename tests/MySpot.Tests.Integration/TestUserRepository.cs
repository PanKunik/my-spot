using MySpot.Core.Entities;
using MySpot.Core.Repositories;
using MySpot.Core.ValueObjects;

namespace MySpot.Tests.Integration;

internal sealed class TestUserRepository : IUserRepository
{
    private readonly List<User> _users = new List<User>();

    public async Task AddAsync(User user)
    {
        _users.Add(user);
        await Task.CompletedTask;
    }

    public async Task<User> GetByEmailAsync(string email)
    {
        await Task.CompletedTask;
        return _users.SingleOrDefault(x => x.Email == email);
    }

    public async Task<User> GetByIdAsync(UserId id)
    {
        await Task.CompletedTask;
        return _users.SingleOrDefault(x => x.UserId == id);
    }

    public async Task<User> GetByUsernameAsync(string username)
    {
        await Task.CompletedTask;
        return _users.SingleOrDefault(x => x.Username == username);
    }
}
