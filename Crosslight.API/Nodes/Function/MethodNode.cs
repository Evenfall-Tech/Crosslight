﻿using Crosslight.API.Nodes.Access;
using Crosslight.API.Nodes.Entities;
using Crosslight.API.Util;
using System;

namespace Crosslight.API.Nodes.Function
{
    /// <summary>
    /// MethodNode represents the method abstraction in the language.
    /// </summary>
    public class MethodNode : FunctionNode, ITypeMember
    {
        public override string Type => nameof(MethodNode);
        public TypeNode OwningType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
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
    }
}
