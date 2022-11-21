using MySpot.Core.Exceptions;

namespace MySpot.Core.ValueObjects;

public record UserId
{
    public Guid Value { get; }

    public UserId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new InvalidEntityIdException(value);
        }

        Value = value;
    }

    public static UserId Create()
        => new(Guid.NewGuid());

    public static implicit operator Guid(UserId data) => data.Value;

    public static implicit operator UserId(Guid value) => new(value);
}