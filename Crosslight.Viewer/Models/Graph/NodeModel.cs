using System.Collections.Generic;

namespace Crosslight.Viewer.Models.Graph
{
    public class NodeModel : ModelBase
    {
        public object Data { get; set; }
        public int ID { get; set; }
        public IEnumerable<int> Connections { get; set; }
    }
}
