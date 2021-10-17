using Crosslight.API.Exceptions;
using Crosslight.API.Nodes;
using Crosslight.API.Nodes.Access;
using Crosslight.API.Nodes.Metadata;
using ICSharpCode.Decompiler.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Text;
using Attribute = ICSharpCode.Decompiler.CSharp.Syntax.Attribute;

namespace Crosslight.Language.CIL.Nodes.Visitors.Syntax.GeneralScope
{
    public class AttributeVisitor : AbstractVisitor<Attribute>
    {
        public AttributeVisitor(VisitContext context) : base(context)
        {

        }

        public override Node Visit(AstNode node)
        {
            if (node == null) throw new NullReferenceException("Passed node was null.");

            try
            {
                if (!(node is Attribute converted))
                    throw new WrongVisitorException(nameof(AttributeVisitor), node.GetType().Name);

                return Visit(converted);
            }
            catch (VisitorException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw new VisitorException(e);
            }
        }

        public override Node Visit(Attribute node)
        {
            try
            {
                var root = new AttributeNode(node.Type.ToString());
                // TODO: parse whole Attribute. Has Type and Arguments.
                foreach (var c in node.Children)
                {
                    var outNode = Context?.VisitFactory?.GetVisitor(c)?.Visit(c);
                    if (outNode != null)
                    {
                        root.Children.Add(outNode);
                    }
                    //throw new NotImplementedException();
                }
                return root;
            }
            catch (Exception e)
            {
                throw new VisitorException(e);
            }
        }

        public override Node VisitAttribute(Attribute node)
        {
            return Visit(node);
        }
    }
}
