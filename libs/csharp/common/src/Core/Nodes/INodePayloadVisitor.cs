namespace Crosslight.Core.Nodes;

public interface INodePayloadVisitor
{
    object? VisitSourceRoot(object context, Node node, SourceRoot payload);
    object? VisitScope(object context, Node node, Scope payload);
    object? VisitHeapType(object context, Node node, HeapType payload);
    object? VisitAccessModifier(object context, Node node, AccessModifier payload);
}
