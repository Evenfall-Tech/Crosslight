using System.Collections.Generic;

namespace Crosslight.Viewer.Models.Graph
{
    public class GraphModel : ModelBase
    {
        public IDictionary<int, NodeModel> Nodes { get; set; }
    }
}
