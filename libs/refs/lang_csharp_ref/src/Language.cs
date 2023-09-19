using Crosslight.Core;
using System.Runtime.InteropServices;
using System.Text;

namespace Crosslight.Lang.CsharpRef;

public class Language : ILanguage
{
    [UnmanagedCallersOnly(EntryPoint = "language_init")]
    public static nint LanguageInit(nint config)
    {
        if (config == 0)
        {
            return 0;
        }

        var language = new Language(new Config(config));
        language._handle = GCHandle.Alloc(language, GCHandleType.Pinned);
        return (nint)language._handle;
    }

    [UnmanagedCallersOnly(EntryPoint = "language_term")]
    public static nuint LanguageTerm(nint context)
    {
        if (context == 0)
        {
            return 1;
        }

        var language = (Language?)GCHandle.FromIntPtr(context).Target;

        if (language == null)
        {
            return 0;
        }

        if (language._handle.IsAllocated)
        {
            language._handle.Free();
        }

        return 1;
    }

    [UnmanagedCallersOnly(EntryPoint = "language_transform_input")]
    public static nint LanguageTransformInput(nint context, nint resource)
    {
        if (context == 0 || resource == 0)
        {
            return 0;
        }

        var input = new Resource(resource);
        var language = (Language?)GCHandle.FromIntPtr(context).Target;

        if (language == null)
        {
            return 0;
        }

        var result = language.TransformInput(input);

        return result == null
            ? 0
            : result.ToPointer();
    }

    [UnmanagedCallersOnly(EntryPoint = "language_transform_output")]
    public static nint LanguageTransformOutput(nint context, nint node)
    {
        if (context == 0 || node == 0)
        {
            return 0;
        }

        return 0;
    }

    [UnmanagedCallersOnly(EntryPoint = "language_transform_modify")]
    public static nint LanguageTransformModify(nint context, nint node)
    {
        if (context == 0 || node == 0)
        {
            return 0;
        }

        return 0;
    }

    [UnmanagedCallersOnly(EntryPoint = "language_resource_types_input")]
    public static nint LanguageResourceTypesInput(nint context)
    {
        if (context == 0)
        {
            return 0;
        }

        var language = (Language?)GCHandle.FromIntPtr(context).Target;

        if (language == null)
        {
            return 0;
        }

        var result = language.ResourceTypesInput;

        return result == null
            ? 0
            : result.ToPointer();
    }

    [UnmanagedCallersOnly(EntryPoint = "language_resource_types_output")]
    public static nint LanguageResourceTypesOutput(nint context)
    {
        if (context == 0)
        {
            return 0;
        }

        var language = (Language?)GCHandle.FromIntPtr(context).Target;

        if (language == null)
        {
            return 0;
        }

        var result = language.ResourceTypesOutput;

        return result == null
            ? 0
            : result.ToPointer();
    }

    [UnmanagedCallersOnly(EntryPoint = "language_resource_types_term")]
    public static nuint LanguageResourceTypesTerm(nint context, nint resourceTypes)
    {
        if (context == 0)
        {
            return 0;
        }

        var language = (Language?)GCHandle.FromIntPtr(context).Target;

        if (language == null)
        {
            return 0;
        }

        return (nuint)(ResourceTypes.TermPointer(resourceTypes) ? 1 : 0);
    }

    private GCHandle _handle;

    public Language(Config config)
    {
    }

    /// <inheritdoc/>
    public Node? TransformInput(Resource resource)
    {
        Console.WriteLine(resource.ContentType);
        Console.WriteLine(Encoding.UTF8.GetString(resource.Content!));

        return null;
    }

    /// <inheritdoc/>
    public Node? TransformModify(Node node)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Resource? TransformOutput(Node node)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public ResourceTypes? ResourceTypesInput => new ResourceTypes(new string[] { "text/plain", "text/x-csharp" });

    /// <inheritdoc/>
    public ResourceTypes? ResourceTypesOutput => new ResourceTypes(Array.Empty<string>());
}
