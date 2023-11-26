using Microsoft.CodeAnalysis.CSharp;

namespace Crosslight.Lang.CsharpRef;

public class LanguageContext
{
    public Stack<CSharpSyntaxNode> ParseOrder { get; }

    public LanguageContext()
    {
        ParseOrder = new Stack<CSharpSyntaxNode>();
    }
}