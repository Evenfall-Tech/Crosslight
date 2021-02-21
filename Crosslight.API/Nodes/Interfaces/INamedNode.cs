using System;
using System.Collections.Generic;
using System.Text;

namespace Crosslight.API.Nodes.Interfaces
{
    public interface INamedNode : INode
    {
        string Name { get; }
    }
}
