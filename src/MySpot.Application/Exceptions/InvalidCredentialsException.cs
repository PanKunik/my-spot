using MySpot.Core.Exceptions;

namespace MySpot.Application.Exceptions;

public sealed class InvalidCredentialsException : CustomException
{
    public InvalidCredentialsException() : base("Invalid credentials") { }
}