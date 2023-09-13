using System;
using System.Runtime.InteropServices;

namespace Crosslight.Lang.CsharpRef;

public class Language
{
    [UnmanagedCallersOnly(EntryPoint = "multiply1")]
    public static int Multiply(int a, int b)
    {
        return a * b;
    }
}