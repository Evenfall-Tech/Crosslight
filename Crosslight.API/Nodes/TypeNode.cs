using System;
using System.Collections.Generic;

namespace Crosslight.API.Nodes
{
    /// <summary>
    /// TypeNode represents the type abstraction in the language.
    /// E.g. in C# it is class/struct, for Java it is class,
    /// in C++ it is class/struct.
    /// </summary>
    public abstract class TypeNode : Node
    {
        public TypeNode()
        {
        }
        public override string ToString()
        {
            return "TypeNode";
        }
    }
}
