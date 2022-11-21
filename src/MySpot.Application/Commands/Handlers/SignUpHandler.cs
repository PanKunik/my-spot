using MySpot.Application.Abstractions;
using MySpot.Application.Exceptions;
using MySpot.Application.Secutiry;
using MySpot.Core.Abstractions;
using MySpot.Core.Entities;
using MySpot.Core.Repositories;
using MySpot.Core.ValueObjects;

namespace MySpot.Application.Commands.Handlers;

internal sealed class SignUpHandler : ICommandHandler<SignUp>
{
    private readonly IClock _clock;
    private readonly IPasswordManager _passwordManager;
    private readonly IUserRepository _userRepository;

    public SignUpHandler(IUserRepository userRepository, IPasswordManager passwordManager, IClock clock)
    {
        _clock = clock;
        this._passwordManager = passwordManager;
        this._userRepository = userRepository;
    }

    public async Task HandleAsync(SignUp command)
    {
        var userId = new UserId(command.UserId);
        var email = new Email(command.Email);
        var username = new Username(command.Username);
        var password = new Password(command.Password);
        var fullName = new FullName(command.FullName);
        var role = string.IsNullOrWhiteSpace(command.Role) ? Role.User() : new Role(command.Role);

        if(await _userRepository.GetByEmailAsync(command.Email) is not null)
        {
            throw new EmailAlreadyInUseException(email);
        }

        if(await _userRepository.GetByUsernameAsync(command.Username) is not null)
        {
            throw new UsernameAlreadyInUseException(username);
        }

        var securedPassword = _passwordManager.Secure(password);
        var user = new User(userId, email, username, securedPassword, fullName, role, _clock.Current());
        await _userRepository.AddAsync(user);
    }
}