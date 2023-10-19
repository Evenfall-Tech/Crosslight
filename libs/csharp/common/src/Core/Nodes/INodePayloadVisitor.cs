﻿namespace Crosslight.Core.Nodes;

public interface INodePayloadVisitor
{
    object? VisitSourceRoot(Node node, SourceRoot payload);
    object? VisitScope(Node node, Scope payload);
    object? VisitHeapType(Node node, HeapType payload);
}
