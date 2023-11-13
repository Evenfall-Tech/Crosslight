namespace Crosslight.Core.Exceptions;

/// <summary>
/// This node configuration is not and will not be supported.
/// </summary>
public class NotSupportedParsingException : ParsingException
{
    public NotSupportedParsingException(Node errorNode)
        : base(errorNode)
    {
    }

    public NotSupportedParsingException(string message, Node errorNode)
        : base(message, errorNode)
    {
    }

    public NotSupportedParsingException(string message, Exception inner, Node errorNode)
        : base(message, inner, errorNode)
    {
    }
}