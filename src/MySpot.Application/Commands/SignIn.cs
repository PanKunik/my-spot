using MySpot.Application.Abstractions;

namespace MySpot.Application.Commands;

public sealed record SignIn(
string Email,
string Password) : ICommand;