using System;
using System.Collections.Generic;

namespace Crosslight.API.Nodes
{
    /// <summary>
    /// ValueNode represents the constant, literal or variable.
    /// </summary>
    public abstract class ValueNode : Node
    {
        public ValueNode()
        {
        }
        public override string ToString()
        {
            return "ValueNode";
        }
    }
}
