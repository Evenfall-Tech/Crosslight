using System;
using System.Collections.Generic;

namespace Crosslight.API.Nodes
{
    /// <summary>
    /// MethodNode represents the method abstraction in the language.
    /// </summary>
    public class MethodNode : FunctionNode, ITypeMember
    {
        public TypeNode parent { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public MethodNode()
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
    }
}
