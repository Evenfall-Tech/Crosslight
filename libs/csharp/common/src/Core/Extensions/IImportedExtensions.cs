using Crosslight.Core.Utilities;
using System.Reflection;

namespace Crosslight.Core.Extensions;

public static class IImportedExtensions
{
    public static bool InvokeMethod(
        this IImported context,
        string methodName,
        object[] parameters,
        out object? result)
    {
        switch (Environment.OSVersion.Platform)
        {
            case PlatformID.Unix:
                methodName += "Unix";
                break;
            case PlatformID.Win32NT:
                methodName += "Win32";
                break;
        }

        MethodInfo? info = context.GetType().GetMethod(methodName);

        if (info != null)
        {
            result = info.Invoke(context, parameters);
            return true;
        }

        result = null;
        return false;
    }
}
