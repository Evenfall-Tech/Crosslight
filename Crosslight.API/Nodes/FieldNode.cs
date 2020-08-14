using System;
using System.Collections.Generic;

namespace Crosslight.API.Nodes
{
    /// <summary>
    /// FieldNode represents the field abstraction in the language.
    /// </summary>
    public class FieldNode : ValueNode, ITypeMember
    {
        public TypeNode parent { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public FieldNode()
        {
        }
        public override string ToString()
        {
            return "FieldNode";
        }
        public override object AcceptVisitor(IVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}
