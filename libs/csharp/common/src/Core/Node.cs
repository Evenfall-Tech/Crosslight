﻿using Crosslight.Core.Nodes;
using Crosslight.src.Core.Nodes;
using System.Runtime.InteropServices;
using static Crosslight.Core.ILanguage;

namespace Crosslight.Core;

public class Node
{
    [StructLayout(LayoutKind.Sequential)]
    private struct NodeImported
    {
        public nint Payload;
        public nuint PayloadType;
        public nint Parent;
        public nint Children;
        public nuint ChildCount;
    }

    /// <summary>
    /// Payload node containing specialized fields.
    /// </summary>
    public INodePayload? Payload { get; }

    /// <summary>
    /// Type of the payload contained in this node.
    /// </summary>
    public NodeType Type => Payload?.Type ?? NodeType.None;

    /// <summary>
    /// Parent of the current node. Used for faster traversing.
    /// </summary>
    public Node? Parent { get; private set; }

    /// <summary>
    /// Children of the current node.
    /// </summary>
    public IReadOnlyCollection<Node>? Children { get; private set; }

    /// <summary>
    /// Create a new node instance as a parent of some children.
    /// </summary>
    /// <param name="children">Initialized set of child nodes to connect to this parent.</param>
    /// <param name="payload">The payload of the created node.</param>
    public Node(INodePayload payload, IEnumerable<Node>? children)
    {
        if (children != null)
        {
            foreach (var child in children)
            {
                child.Parent = this;
            }

            Children = children.ToList();
        }

        Payload = payload;
    }

    /// <summary>
    /// Create a new node instance from a native representation.
    /// </summary>
    /// <param name="pointer">The native pointer to a node.</param>
    /// <param name="nodeMapping">Mapping of node types to <c>typeof(INodePayload)</c> derivatives.</param>
    /// <param name="parent">Parent node, instantiated beforehand.</param>
    /// <param name="parseChildren">Whether children of the current node should be parsed.</param>
    /// <param name="parseUnsupported">Whether unsupported nodes should be parsed.</param>
    public Node(
        nint pointer,
        IReadOnlyDictionary<uint, Func<nint, INodePayload?>> nodeMapping,
        Node? parent = null,
        bool parseChildren = false,
        bool parseUnsupported = true)
    {
        NodeImported imported = Marshal.PtrToStructure<NodeImported>(pointer);

        Parent = parent; // Ignore parent pointer.
        int childCount = (int)imported.ChildCount;
        var offset = Marshal.SizeOf<NodeImported>();

        if (imported.Payload != 0 && imported.PayloadType != 0)
        {
            if (nodeMapping.TryGetValue((uint)imported.PayloadType, out var payloadFactory))
            {
                Payload = payloadFactory(imported.Payload);
            }
            else if (!parseUnsupported)
            {
                throw new NotImplementedException($"Payload type {(uint)imported.PayloadType} is not yet supported.");
            }
        }

        if (parseChildren && imported.Children != 0 && childCount > 0)
        {
            List<Node> children = new();

            for (int i = 0; i < childCount; ++i)
            {
                children.Add(new Node(imported.Children + i * offset, nodeMapping, this, parseChildren, parseUnsupported));
            }

            Children = children;
        }
    }

    /// <summary>
    /// Convert this node and all its children to a native pointer.
    /// </summary>
    /// <param name="acquire">Delegate to allocate memory for the node, its contents and children.</param>
    /// <param name="parentPtr">Pointer to the parent node, if exists.</param>
    /// <param name="currentPtr">Pointer to the current node, if allocated elsewhere.</param>
    /// <returns>The native pointer, leading to the allocated native representation of the node.</returns>
    /// <remarks>
    /// If a memory allocation error occurs anywhere apart from the initial node memory allocation,
    /// the function returns a pointer to a partially converted structure. Otherwise, if the initial
    /// allocation failed, the function returns <c>0</c>.
    /// </remarks>
    public nint ToPointer(AcquireDelegate? acquire = null, nint parentPtr = 0, nint? currentPtr = null)
    {
        acquire ??= Marshal.AllocCoTaskMem;
        var offset = Marshal.SizeOf<NodeImported>();
        currentPtr ??= acquire(offset);

        if (currentPtr.Value == 0)
        {
            return 0;
        }

        nint payloadPtr = 0;

        if (Payload != null)
        {
            payloadPtr = Payload.ToPointer(acquire);
        }

        nint childrenPtr = 0;

        if (Children != null && Children.Count > 0)
        {
            childrenPtr = acquire(Children.Count * offset);
            int i = 0;

            if (childrenPtr != 0)
            {
                foreach (var child in Children)
                {
                    child.ToPointer(acquire, currentPtr.Value, childrenPtr + i * offset);
                    ++i;
                }
            }
        }

        NodeImported resource = new()
        {
            Payload = payloadPtr,
            PayloadType = (nuint)Type,
            ChildCount = (nuint)(Children?.Count ?? 0),
            Children = childrenPtr,
            Parent = parentPtr,
        };

        Marshal.StructureToPtr(resource, currentPtr.Value, false);

        return currentPtr.Value;
    }

    /// <summary>
    /// Get the strongly-typed payload of the current node.
    /// </summary>
    /// <typeparam name="TPayload">Type of the payload to get.</typeparam>
    /// <returns>The payload instance, if it exists and the type is correct. Otherwise <see langword="null"/>.</returns>
    public TPayload? GetPayload<TPayload>() where TPayload : struct, INodePayload
    {
        if (Payload == null)
        {
            return null;
        }

        if (Payload is TPayload payload)
        {
            return payload;
        }

        return null;
    }

    public static IReadOnlyCollection<nint>? GetChildren(nint pointer)
    {
        NodeImported imported = Marshal.PtrToStructure<NodeImported>(pointer);

        int childCount = (int)imported.ChildCount;
        var offset = Marshal.SizeOf<NodeImported>();

        if (imported.Children != 0 && childCount > 0)
        {
            List<nint> children = new();

            for (int i = 0; i < childCount; ++i)
            {
                children.Add(imported.Children + i * offset);
            }

            return children;
        }

        return null;
    }

    public static IReadOnlyDictionary<uint, Func<nint, INodePayload?>> GetDefaultPayloadMapping()
    {
        return new Dictionary<uint, Func<nint, INodePayload?>>()
        {
            { (uint)NodeType.None, (nint _) => null },
            { (uint)NodeType.SourceRoot, (nint p) => p == 0 ? null : new SourceRoot(p) },
        };
    }
}
