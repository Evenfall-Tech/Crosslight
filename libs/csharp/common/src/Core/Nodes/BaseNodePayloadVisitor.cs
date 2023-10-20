namespace Crosslight.Core.Nodes;

public abstract class BaseNodePayloadVisitor : INodePayloadVisitor
{
    public abstract object? VisitSourceRoot(Node node, SourceRoot payload);
    
    public abstract object? VisitScope(Node node, Scope payload);
    
    public abstract object? VisitHeapType(Node node, HeapType payload);

    protected virtual object? GetDefaultResult() => null;

    protected virtual object? VisitChildren(Node node)
    {
        var result = GetDefaultResult();

        if (node.HasChildren)
        {
            return node.Children!
                .Select(child => child.Payload == null
                    ? GetDefaultResult()
                    : child.Payload.AcceptVisitor(child, this))
                .Aggregate(result, AggregateResults);
        }

        return result;
    }

    protected virtual object? AggregateResults(object? aggregate, object? next) => next;
}