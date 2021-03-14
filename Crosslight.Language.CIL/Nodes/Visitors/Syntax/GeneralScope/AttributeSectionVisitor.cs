using Crosslight.API.Exceptions;
using Crosslight.API.Nodes;
using Crosslight.API.Nodes.Access;
using Crosslight.API.Nodes.Metadata;
using ICSharpCode.Decompiler.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Attribute = ICSharpCode.Decompiler.CSharp.Syntax.Attribute;

namespace Crosslight.Language.CIL.Nodes.Visitors.Syntax.GeneralScope
{
    public class AttributeSectionVisitor : AbstractVisitor<AttributeSection>
    {
        public AttributeSectionVisitor(VisitContext context) : base(context)
        {

        }

        public override Node Visit(AstNode node)
        {
            if (node == null) throw new NullReferenceException("Passed node was null.");

            try
            {
                if (!(node is AttributeSection converted))
                    throw new WrongVisitorException(nameof(AttributeSectionVisitor), node.GetType().Name);

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

        public override Node Visit(AttributeSection node)
        {
            try
            {
                var root = new RootNode();
                var options = GetAttributeOptions(node);
                foreach (var c in node.Attributes)
                {
                    var visitor = Context?.VisitFactory?.GetVisitor(nameof(Attribute)) as ICILVisitor<Attribute>;
                    var outNode = c.AcceptVisitor(visitor);
                    if (outNode is AttributeNode at)
                    {
                        at.Options.Target = options.Target;
                        root.Children.Add(at);
                    }
                    else
                    {
                        throw new NotImplementedException();
                    }
                }
                foreach (var c in node.Children.Except(node.Attributes).Except(new AstNode[] { node.AttributeTargetToken }))
                {
                    var outNode = Context?.VisitFactory?.GetVisitor(c)?.Visit(c);
                    root.Children.Add(outNode);
                }
                return root;
            }
            catch (Exception e)
            {
                throw new VisitorException(e);
            }
        }

        public override Node VisitAttributeSection(AttributeSection attributeSection)
        {
            return Visit(attributeSection);
        }

        private AttributeOptions GetAttributeOptions(AttributeSection node)
        {
            // TODO: apply all options of attributes.
            return new AttributeOptions()
            {
                Target = TargetFromString(node.AttributeTarget),
            };
        }

        private AttributeTarget TargetFromString(string target)
        {
            switch (target)
            {
                case "assembly":
                    return AttributeTarget.Project;
                case "module":
                    return AttributeTarget.Module;
                case "field":
                    return AttributeTarget.Field;
                case "event":
                    return AttributeTarget.Event;
                case "method":
                    return AttributeTarget.Method;
                case "param":
                    return AttributeTarget.Param;
                case "property":
                    return AttributeTarget.Property;
                case "return":
                    return AttributeTarget.Return;
                case "type":
                    return AttributeTarget.Type;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
