using System.Collections.Generic;

namespace Crosslight.Viewer.Models.Graph
{
    public class GraphModel : ModelBase
    {
        public ICollection<NodeModel> Nodes { get; set; }
    }
}
