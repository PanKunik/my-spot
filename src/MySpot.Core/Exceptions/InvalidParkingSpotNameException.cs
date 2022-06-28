namespace MySpot.Core.Exceptions;

public sealed class InvalidParkingSpotNameException : CustomException
{
    public InvalidParkingSpotNameException()
        : base("Parking spot name is invalid.")
    {

    }
}