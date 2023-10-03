using Crosslight.Core;
using System.Runtime.InteropServices;

namespace Crosslight.Lang.CsharpRef;

public static class LanguageExported
{
    [UnmanagedCallersOnly(EntryPoint = "language_init")]
    public static nint LanguageInit(nint config)
    {
        if (config == 0)
        {
            return 0;
        }

        var configObject = new Config(config);
        var options = new LanguageOptions(configObject);
        var language = new Language(options);
        return (nint)language.Handle;
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

        if (language.Handle.IsAllocated)
        {
            Language.Options.Remove(language);
            language.Handle.Free();
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

        var language = (Language?)GCHandle.FromIntPtr(context).Target;

        if (language == null)
        {
            return 0;
        }

        var input = new Resource(resource);
        var result = language.TransformInput(input);

        return result == null
            ? 0
            : result.ToPointer(Language.Options[language].Acquire);
    }

    [UnmanagedCallersOnly(EntryPoint = "language_transform_output")]
    public static nint LanguageTransformOutput(nint context, nint node)
    {
        if (context == 0 || node == 0)
        {
            return 0;
        }

        var language = (Language?)GCHandle.FromIntPtr(context).Target;

        if (language == null)
        {
            return 0;
        }

        Node nodeInstance;

        try
        {
            nodeInstance = new Node(
                node,
                Node.GetDefaultPayloadMapping(),
                parent: null,
                parseChildren: true,
                parseUnsupported: Language.Options[language].ParseUnsupported);
        }
        catch
        {
            return 0;
        }

        var result = language.TransformOutput(nodeInstance);

        return result == null
            ? 0
            : result.ToPointer(Language.Options[language].Acquire);
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
            : result.ToPointer(Language.Options[language].Acquire);
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
            : result.ToPointer(Language.Options[language].Acquire);
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

        return (nuint)(ResourceTypes.TermPointer(resourceTypes, Language.Options[language].Release) ? 1 : 0);
    }
}
