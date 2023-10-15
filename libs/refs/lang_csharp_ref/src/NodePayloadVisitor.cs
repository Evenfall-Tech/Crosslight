using Crosslight.Core;
using Crosslight.Core.Nodes;
using Crosslight.src.Core.Nodes;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Crosslight.Lang.CsharpRef
{
    internal class NodePayloadVisitor : BaseNodePayloadVisitor
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

        public override object? VisitSourceRoot(Node node, SourceRoot payload)
        {
            var syntax = SyntaxFactory.CompilationUnit();

            if (payload.FileName != null)
            {
                syntax = syntax.WithLeadingTrivia(SyntaxFactory.Trivia(
                    SyntaxFactory.DocumentationComment(
                        SyntaxFactory.XmlText($" {payload.FileName}"))));
            }

            if (node.HasChildren)
            {
                if (VisitChildren(node) is IEnumerable<CSharpSyntaxNode> children)
                {
                    var nodes = children.ToArray();
                    syntax = syntax
                        .AddUsings(nodes.OfType<UsingDirectiveSyntax>().ToArray())
                        //.AddAttributeLists(nodes.OfType<AttributeListSyntax>().ToArray())
                        //.AddExterns(nodes.OfType<ExternAliasDirectiveSyntax>().ToArray())
                        .AddMembers(nodes.OfType<MemberDeclarationSyntax>().ToArray());
                }
            }

            _rootNode ??= syntax;

            return new CSharpSyntaxNode[] { syntax };
        }

        public override object? VisitScope(Node node, Scope payload)
        {
            var identifier = SyntaxFactory.IdentifierName(
                payload.Identifier
                ?? throw new NotSupportedException(
                    "Namespaces without identifiers are not supported."));
            BaseNamespaceDeclarationSyntax syntax = node.HasChildren
                ? SyntaxFactory.NamespaceDeclaration(identifier)
                : SyntaxFactory.FileScopedNamespaceDeclaration(identifier);

            if (node.HasChildren)
            {
                if (VisitChildren(node) is IEnumerable<CSharpSyntaxNode> children)
                {
                    // TODO: parse children.
                }
            }

            _rootNode ??= syntax;

            return new CSharpSyntaxNode[] { syntax };
        }

        #region Overrides

        protected override object? GetDefaultResult()
        {
            return Array.Empty<CSharpSyntaxNode>();
        }

        protected override object? AggregateResults(object? aggregate, object? next)
        {
            var container = (IEnumerable<CSharpSyntaxNode>)aggregate!;

            if (next != null)
            {
                container = container.Concat((IEnumerable<CSharpSyntaxNode>)next);
            }

            return container;
        }

        #endregion
    }
}
