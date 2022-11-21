using MySpot.Core.Entities;
using MySpot.Core.ValueObjects;

namespace MySpot.Core.Repositories;
public interface IUserRepository
{
    Task<User> GetByIdAsync(UserId id);
    Task<User> GetByEmailAsync(string email);
    Task<User> GetByUsernameAsync(string username);
    Task AddAsync(User user);
}