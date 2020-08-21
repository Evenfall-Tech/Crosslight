using System;
using System.Collections.Generic;
using System.Text;

namespace Crosslight.API.Nodes.Access
{
    interface ITypeMember
    {
        TypeNode parent { get; set; }
    }
}
