using Crosslight.API.Nodes.Access;
using Crosslight.API.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace Crosslight.API.Nodes.Interfaces
{
    public interface IModifiedNode : INode
    {
        SyncedList<ModifierNode, Node> Modifiers { get; }
    }
}
