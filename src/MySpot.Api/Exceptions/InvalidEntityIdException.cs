namespace MySpot.Api.Exceptions;

public sealed class InvalidEntityIdException : CustomException
{
    public object EntityId { get; }

    public InvalidEntityIdException(object entityId)
        : base($"Cannot set: { entityId } as entity identifier.")
    {
        EntityId = entityId;
    }
}