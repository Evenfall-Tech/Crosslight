using Crosslight.API.Nodes.Access;
using Crosslight.API.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace Crosslight.API.Nodes.Interfaces
{
    public interface IAttributedNode : INode
    {
        SyncedList<AttributeNode, Node> Attributes { get; }
    }
}
