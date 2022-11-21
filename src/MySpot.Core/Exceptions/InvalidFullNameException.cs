namespace MySpot.Core.Exceptions;

public sealed class InvalidFullNameException : CustomException
{
    public string FullName { get; }

    public InvalidFullNameException(string fullName)
        :base($"")
    {
        FullName = fullName;
    }
}