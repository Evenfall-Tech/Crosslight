using Crosslight.Core;
using System.Collections.Concurrent;
using System.Runtime.InteropServices;
using System.Text;

namespace Crosslight.Lang.CsharpRef;

internal class Language : ILanguage
{
    public static IDictionary<Language, LanguageOptions> Options { get; }

    public GCHandle Handle { get; private set; }

    static Language()
    {
        Options = new ConcurrentDictionary<Language, LanguageOptions>();
    }

    public Language(LanguageOptions options)
    {
        Handle = GCHandle.Alloc(this, GCHandleType.Pinned);
        Options[this] = options;
    }

    /// <inheritdoc/>
    public Node? TransformInput(Resource resource)
    {
        if (ResourceTypesInput?.ContentTypes?.Any(x => x == resource.ContentType) == true)
        {
            return null;
        }

        return null;
    }

    /// <inheritdoc/>
    public Node? TransformModify(Node node)
    {
        return null;
    }

    /// <inheritdoc/>
    public Resource? TransformOutput(Node node)
    {
        var visitor = new NodePayloadVisitor(Options[this]);
        node.Payload?.AcceptVisitor(visitor);
        var text = visitor.Text;

        if (text == null)
        {
            return null;
        }

        List<byte> bytes = new(Encoding.UTF8.GetBytes(text))
        {
            0
        };

        return new Resource(bytes.ToArray(), "text/plain");
    }

    /// <inheritdoc/>
    public ResourceTypes? ResourceTypesInput => new(Array.Empty<string>());

    /// <inheritdoc/>
    public ResourceTypes? ResourceTypesOutput => new(new string[] { "text/plain", "text/x-csharp" });
}
