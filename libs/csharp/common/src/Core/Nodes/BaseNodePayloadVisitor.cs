namespace Crosslight.Core.Nodes;

public abstract class BaseNodePayloadVisitor : INodePayloadVisitor
{
    public abstract object? VisitSourceRoot(object context, Node node, SourceRoot payload);
    
    public abstract object? VisitScope(object context, Node node, Scope payload);
    
    public abstract object? VisitHeapType(object context, Node node, HeapType payload);

    public abstract object? VisitAccessModifier(object context, Node node, AccessModifier payload);

    protected virtual object? GetDefaultResult() => null;

    protected virtual object? VisitChildren(object context, Node node)
    {
        var result = GetDefaultResult();

        if (node.HasChildren)
        {
            return node.Children!
                .Select(child => child.Payload == null
                    ? GetDefaultResult()
                    : child.Payload.AcceptVisitor(context, child, this))
                .Aggregate(result, (acc, src) => AggregateResults(context, acc, src));
        }

        return result;
    }

    protected virtual object? AggregateResults(object context, object? aggregate, object? next) => next;
}