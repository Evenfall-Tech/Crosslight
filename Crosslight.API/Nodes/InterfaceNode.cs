﻿using System;
using System.Collections.Generic;

namespace Crosslight.API.Nodes
{
    /// <summary>
    /// InterfaceNode represents the interface abstraction in the language.
    /// </summary>
    public class InterfaceNode : TypeNode
    {
        public override Type Type => typeof(InterfaceNode);
        public InterfaceNode()
        {
        }
        public override string ToString()
        {
            return "InterfaceNode";
        }
        public override object AcceptVisitor(IVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}
