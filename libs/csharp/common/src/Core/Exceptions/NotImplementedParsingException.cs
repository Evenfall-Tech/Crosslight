namespace Crosslight.Core.Exceptions;

/// <summary>
/// Parsing for this node configuration has not yet been implemented.
/// </summary>
public class NotImplementedParsingException : ParsingException
{
    public NotImplementedParsingException(Node errorNode)
        : base(errorNode)
    {
    }

    public NotImplementedParsingException(string message, Node errorNode)
        : base(message, errorNode)
    {
    }

    public NotImplementedParsingException(string message, Exception inner, Node errorNode)
        : base(message, inner, errorNode)
    {
    }
}