using Crosslight.Core;
using Crosslight.Core.Exceptions;
using Crosslight.Core.Nodes;
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

        public override object VisitSourceRoot(object context, Node node, SourceRoot payload)
        {
            var syntax = SyntaxFactory.CompilationUnit();
            bool setRoot = _rootNode == null;

            if (payload.FileName != null)
            {
                syntax = syntax.WithLeadingTrivia(SyntaxFactory.Trivia(
                    SyntaxFactory.DocumentationComment(
                        SyntaxFactory.XmlText($" {payload.FileName}"))));
            }

            if (node.HasChildren)
            {
                var stack = ((LanguageContext)context).ParseOrder;
                stack.Push(syntax);
                
                if (VisitChildren(context, node) is IEnumerable<CSharpSyntaxNode> children)
                {
                    syntax = (CompilationUnitSyntax)stack.Peek();
                    var nodes = children.ToArray();
                    syntax = syntax
                        .AddUsings(nodes.OfType<UsingDirectiveSyntax>().ToArray())
                        //.AddAttributeLists(nodes.OfType<AttributeListSyntax>().ToArray())
                        //.AddExterns(nodes.OfType<ExternAliasDirectiveSyntax>().ToArray())
                        .AddMembers(nodes.OfType<MemberDeclarationSyntax>().ToArray());
                }

                stack.Pop();
            }

            if (setRoot)
            {
                _rootNode = syntax;
            }

            return new CSharpSyntaxNode[] { syntax };
        }

        public override object VisitScope(object context, Node node, Scope payload)
        {
            if (payload.Identifier == null)
            {
                switch (_options.UnsupportedBehavior)
                {
                    case UnsupportedBehaviorType.Skip:
                        return GetDefaultResult();
                    case UnsupportedBehaviorType.Pass:
                        return VisitChildren(context, node) ?? GetDefaultResult();
                    case UnsupportedBehaviorType.Throw:
                    default:
                        throw new NotSupportedParsingException(
                            "Namespaces without identifiers are not supported.", node);
                }
            }
            
            var identifier = SyntaxFactory.IdentifierName(payload.Identifier);
            BaseNamespaceDeclarationSyntax syntax = node.HasChildren
                ? SyntaxFactory.NamespaceDeclaration(identifier)
                : SyntaxFactory.FileScopedNamespaceDeclaration(identifier);
            bool setRoot = _rootNode == null;

            if (node.HasChildren)
            {
                var stack = ((LanguageContext)context).ParseOrder;
                stack.Push(syntax);
                
                if (VisitChildren(context, node) is IEnumerable<CSharpSyntaxNode> children)
                {
                    syntax = (BaseNamespaceDeclarationSyntax)stack.Peek();
                    var nodes = children.ToArray();
                    syntax = syntax
                        .AddMembers(nodes.OfType<MemberDeclarationSyntax>().ToArray());
                }

                stack.Pop();
            }

            if (setRoot)
            {
                _rootNode = syntax;
            }

            return new CSharpSyntaxNode[] { syntax };
        }

        public override object VisitHeapType(object context, Node node, HeapType payload)
        {
            if (payload.Identifier == null)
            {
                switch (_options.UnsupportedBehavior)
                {
                    case UnsupportedBehaviorType.Skip:
                        return GetDefaultResult();
                    case UnsupportedBehaviorType.Pass:
                        return VisitChildren(context, node) ?? GetDefaultResult();
                    case UnsupportedBehaviorType.Throw:
                    default:
                        throw new NotSupportedParsingException(
                            "Heap types without identifiers are not supported.", node);
                }
            }
            
            var syntax = SyntaxFactory.ClassDeclaration(payload.Identifier);
            bool setRoot = _rootNode == null;

            if (node.HasChildren)
            {
                var stack = ((LanguageContext)context).ParseOrder;
                stack.Push(syntax);

                if (VisitChildren(context, node) is IEnumerable<CSharpSyntaxNode> children)
                {
                    syntax = (ClassDeclarationSyntax)stack.Peek();
                    var nodes = children.ToArray();
                    syntax = syntax
                        .AddMembers(nodes.OfType<MemberDeclarationSyntax>().ToArray());
                }

                stack.Pop();
            }

            if (setRoot)
            {
                _rootNode = syntax;
            }

            return new CSharpSyntaxNode[] { syntax };
        }

        public override object VisitAccessModifier(object context, Node node, AccessModifier payload)
        {
            var stack = ((LanguageContext)context).ParseOrder;
            if (!stack.TryPop(out var parent))
            {
                switch (_options.UnsupportedBehavior)
                {
                    case UnsupportedBehaviorType.Skip:
                        return GetDefaultResult();
                    case UnsupportedBehaviorType.Pass:
                        return VisitChildren(context, node) ?? GetDefaultResult();
                    case UnsupportedBehaviorType.Throw:
                    default:
                        throw new NotSupportedParsingException(
                            "Dangling access modifiers are not supported. Add a parent node.", node);
                }
            }
            
            var modifier = SyntaxFactory.Token(payload.Kind switch
            {
                AccessModifierType.None => SyntaxKind.None,
                AccessModifierType.Public => SyntaxKind.PublicKeyword,
                AccessModifierType.Protected => SyntaxKind.ProtectedKeyword,
                AccessModifierType.Private => SyntaxKind.PrivateKeyword,
                _ => SyntaxKind.None,
            });

            if (parent is ClassDeclarationSyntax classSyntax)
            {
                classSyntax = classSyntax.AddModifiers(modifier);
                stack.Push(classSyntax);
            }
            else
            {
                switch (_options.UnsupportedBehavior)
                {
                    case UnsupportedBehaviorType.Skip:
                        return GetDefaultResult();
                    case UnsupportedBehaviorType.Pass:
                        return VisitChildren(context, node) ?? GetDefaultResult();
                    case UnsupportedBehaviorType.Throw:
                    default:
                        throw new NotImplementedParsingException(
                            $"Access modifiers for {parent.GetType()} are not supported.", node);
                }
            }

            if (node.HasChildren)
            {
                if (VisitChildren(context, node) is IEnumerable<CSharpSyntaxNode> children)
                {
                    var nodes = children.ToArray();
                    // Note: no children for access modifiers are currently supported.
                }
            }

            // Explicitly return empty array instead of default result
            // to avoid regression errors if default result changes.
            // This shows that the function did some processing, although
            // no new syntax nodes were produced.
            return Array.Empty<CSharpSyntaxNode>();
        }

        #region Overrides

        protected override object GetDefaultResult()
        {
            return Array.Empty<CSharpSyntaxNode>();
        }

        protected override object AggregateResults(object context, object? aggregate, object? next)
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
