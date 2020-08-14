using System;
using System.Collections.Generic;
using System.Text;

namespace Crosslight.API.Nodes
{
    interface ITypeMember
    {
        TypeNode parent { get; set; }
    }
}
