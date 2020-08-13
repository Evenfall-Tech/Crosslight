using System;
using System.Collections.Generic;
using System.Text;

namespace Crosslight.API.Nodes
{
    public interface IVisitor
    {
        object Visit(Node node);
    }
}
