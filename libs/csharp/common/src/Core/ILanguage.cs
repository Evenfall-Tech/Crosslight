namespace Crosslight.Core;

public interface ILanguage
{
    /// <summary>
    /// Transform an input resource to a node tree.
    /// </summary>
    /// <param name="resource">Input resource to transform.</param>
    /// <returns>A transformed Crosslight node tree or <c>0</c> on error.</returns>
    Node? TransformInput(Resource resource);

    /// <summary>
    /// Transform a Crosslight node tree to an output resource.
    /// </summary>
    /// <param name="node">Node tree to transform.</param>
    /// <returns>A transformed output resource or <c>0</c> on error.</returns>
    Resource? TransformOutput(Node node);

    /// <summary>
    /// Transform a Crosslight node tree to a different form.
    /// </summary>
    /// <param name="node">Node tree to transform.</param>
    /// <returns>A transformed Crosslight node tree or <c>0</c> on error.</returns>
    Node? TransformModify(Node node);

    /// <summary>
    /// Get a set of supported MIME-types for language input resources.
    /// </summary>
    ResourceTypes? ResourceTypesInput { get; }

    /// <summary>
    /// Get a set of supported MIME-types for language output resources.
    /// </summary>
    ResourceTypes? ResourceTypesOutput { get; }

    /// <summary>
    /// Delegate to allocate a piece of memory.
    /// </summary>
    /// <param name="size">Size of allocated memory in bytes.</param>
    /// <returns>The pointer to the allocated memory or <c>0</c> on error.</returns>
    public delegate nint AcquireDelegate(int size);

    /// <summary>
    /// Delegate to free a piece of memory.
    /// </summary>
    /// <param name="pointer">The pointer to the memory, previously allocated with <see cref="AcquireDelegate"/>.</param>
    public delegate void ReleaseDelegate(nint pointer);
}
