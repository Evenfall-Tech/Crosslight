using Crosslight.API.Nodes.Access;
using Crosslight.API.Nodes.Entities;
using Crosslight.API.Util;
using System;

namespace Crosslight.API.Nodes.Function
{
    /// <summary>
    /// <see cref="MethodNode"/> represents the method abstraction in the language.
    /// </summary>
    public class MethodNode : FunctionNode, ITypeMember
    {
        public override string Type => nameof(MethodNode);
        public FunctionalTypeNode OwningType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public MethodNode(string name) : base(name)
        {
        }
        public override string ToString()
        {
            return "MethodNode";
        }
        public override object AcceptVisitor(IVisitor visitor)
        {
            return visitor.Visit(this);
        }
        public override S AcceptVisitor<S>(IVisitor<S> visitor)
        {
            return visitor.Visit(this);
        }
        public override S AcceptVisitor<T, S>(IVisitor<T, S> visitor, T data)
        {
            return visitor.Visit(this, data);
        }
    }
}
