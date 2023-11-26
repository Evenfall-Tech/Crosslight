namespace Crosslight.Core.Exceptions;

public class ParsingException : Exception
{
    protected IEnumerable<Node> _stack;
    
    public ParsingException(Node errorNode)
    {
        _stack = Node.CollectParsingStack(errorNode);
    }

    public ParsingException(string message, Node errorNode)
        : base(message)
    {
        _stack = Node.CollectParsingStack(errorNode);
    }

    public ParsingException(string message, Exception inner, Node errorNode)
        : base(message, inner)
    {
        _stack = Node.CollectParsingStack(errorNode);
    }
    
    public override String Message {
        get {  
            return base.Message +
                string.Join(
                    '\n',
                    _stack.Select((x, i) => string.Empty.PadLeft(i, '\t') + x.ToString(false))) +
                '\n';
        }
    }
}