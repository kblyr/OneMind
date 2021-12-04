using System.Runtime.Serialization;

namespace OneMind.Exceptions;

public class UserCreationFailedException : Exception
{
    public UserCreationFailedException()
    {
    }

    public UserCreationFailedException(string? message) : base(message)
    {
    }

    public UserCreationFailedException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected UserCreationFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}