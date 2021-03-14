using System.Collections.Generic;

namespace Crosslight.Language.Viewer.Models.Graph
{
    public class NodeModel : ModelBase
    {
        public object Data { get; set; }
        public string Type { get; set; }
        public int ID { get; set; }
        public ICollection<int> Connections { get; set; }
    }
}
