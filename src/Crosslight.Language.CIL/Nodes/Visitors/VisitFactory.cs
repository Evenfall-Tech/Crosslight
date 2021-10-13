using Crosslight.Language.CIL.Nodes.Visitors.Syntax;
using Crosslight.Language.CIL.Nodes.Visitors.Syntax.GeneralScope;
using ICSharpCode.Decompiler.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using Attribute = ICSharpCode.Decompiler.CSharp.Syntax.Attribute;

namespace Crosslight.Language.CIL.Nodes.Visitors
{
    /// <summary>
    /// A factory to create visitors for AST nodes.
    /// </summary>
    public class VisitFactory
    {
        private readonly Dictionary<string, Func<VisitContext, ICILVisitor>> visitorConstructors;
        private readonly VisitContext context;
        private readonly GeneralDummyVisitor generalDummyVisitor;
        private Dictionary<string, ICILVisitor> visitors;
        private Dictionary<string, ICILVisitor> Visitors
        {
            get
            {
                if (visitors == null || visitors.Count != visitorConstructors.Count)
                    ConstructAllVisitors(context);
                return visitors;
            }
        }
        /// <summary>
        /// Create a factory using a visitor as dispatcher.
        /// </summary>
        /// <param name="owner">Owning visitor to use for dispatch.</param>
        public VisitFactory(VisitContext context)
        {
            this.context = context;
            generalDummyVisitor = new GeneralDummyVisitor();
            visitorConstructors = new Dictionary<string, Func<VisitContext, ICILVisitor>>()
            {
                { nameof(SyntaxTree), (c) => new SyntaxTreeVisitor(c) },
                { nameof(NamespaceDeclaration), (c) => new NamespaceDeclarationVisitor(c) },
                { nameof(DelegateDeclaration), (c) => new DelegateDeclarationVisitor(c) },
                { nameof(TypeDeclaration), (c) => new TypeDeclarationVisitor(c) },
                { nameof(AttributeSection), (c) => new AttributeSectionVisitor(c) },
                { nameof(Attribute), (c) => new AttributeVisitor(c) },
            };
        }

        private void ConstructAllVisitors(VisitContext dispatcher)
        {
            visitors = visitorConstructors
                .Select(vc => new { vc.Key, Value = vc.Value(dispatcher) })
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }
        /// <summary>
        /// Get all visitors, registered in the factory.
        /// </summary>
        /// <returns>An <seealso cref="IEnumerable{T}"/> of visitors for node types.</returns>
        public IEnumerable<ICILVisitor> GetVisitors()
        {
            return Visitors.Values;
        }
        /// <summary>
        /// Get a visitor from cache for a certain node type.
        /// </summary>
        /// <param name="visiteeName">Name of node type to visit.</param>
        public ICILVisitor GetVisitor(string visiteeName)
        {
            return Visitors[visiteeName];
        }
        /// <summary>
        /// Get a visitor from cache for a certain node type.
        /// </summary>
        /// <typeparam name="T">Node type to get visitor for.</typeparam>
        public ICILVisitor<T> GetVisitor<T>() where T : AstNode
        {
            return Visitors[typeof(T).Name] as ICILVisitor<T>;
        }
        /// <summary>
        /// Get a newly constructed visitor for a certain node type.
        /// </summary>
        /// <param name="visiteeName">Name of node type to visit.</param>
        public ICILVisitor GetNewVisitor(string visiteeName)
        {
            return visitorConstructors[visiteeName](context);
        }
        /// <summary>
        /// Get a visitor from cache for a certain node that can be visited by it.
        /// </summary>
        /// <param name="node">Node to visit.</param>
        public ICILVisitor GetVisitor(AstNode node)
        {
            foreach (var v in Visitors.Values)
            {
                if (v.CanBeVisited(node)) return v;
            }
            // TODO: replace with a generic visitor that outputs dummy nodes or throw KeyNotFoundException.
            return generalDummyVisitor;
        }
    }
}
