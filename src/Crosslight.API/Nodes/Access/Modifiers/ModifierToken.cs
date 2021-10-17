namespace Crosslight.API.Nodes.Access.Modifiers
{
    public enum ModifierToken
    {
        None,
        Custom,
        // Access
        Internal,
        Public,
        Protected,
        Private,
        Readonly,
        // Inheritance control
        Abstract,
        Virtual,
        Sealed,
        New,
        Override,
        Static,
        // Conversion type
        Implicit,
        Explicit,
        // Parallelism
        Async,
        // Optimizations
        Volatile,
        Unsafe,
        Extern,
        // Parameter passing
        In,
        Out,
        Ref,
        Params,
    }
}
