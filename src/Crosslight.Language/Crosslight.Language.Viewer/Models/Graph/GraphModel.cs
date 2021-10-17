using System.Collections.Generic;

namespace Crosslight.Language.Viewer.Models.Graph
{
    public class GraphModel : ModelBase
    {
        public IDictionary<int, NodeModel> Nodes { get; set; }
    }
}
