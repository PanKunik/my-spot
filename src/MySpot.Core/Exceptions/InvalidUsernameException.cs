namespace MySpot.Core.Exceptions;

public sealed class InvalidUsernameException : CustomException
{
    public string Username { get; }

    public InvalidUsernameException(string username) : base($"Username: '{username}' is invalid.")
    {
        Username = username;
    }
}