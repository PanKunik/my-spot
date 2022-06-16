namespace MySpot.Api.Exceptions;

public sealed class InvalidParkingSpotNameException : CustomException
{
    public InvalidParkingSpotNameException()
        : base($"Parking spot name is invalid.")
    {
        
    }
}