using System;
using System.Collections.Generic;
using System.Text;

namespace Crosslight.Viewer.Models.Graph
{
    public class GraphModel : ModelBase
    {
        public ICollection<NodeModel> Nodes { get; set; }
    }
}
