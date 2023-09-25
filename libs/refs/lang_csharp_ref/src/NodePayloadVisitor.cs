using Crosslight.Core.Nodes;
using Crosslight.src.Core.Nodes;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Crosslight.Lang.CsharpRef
{
    internal class NodePayloadVisitor : INodePayloadVisitor
    {
        private readonly LanguageOptions _options;
        private CSharpSyntaxNode? _rootNode;

        public string? Text => _rootNode?
            .NormalizeWhitespace()
            .ToFullString();

        public NodePayloadVisitor(LanguageOptions options)
        {
            _options = options;
        }

        public void VisitSourceRoot(SourceRoot payload)
        {
            _rootNode = SyntaxFactory.CompilationUnit();
        }
    }
}
