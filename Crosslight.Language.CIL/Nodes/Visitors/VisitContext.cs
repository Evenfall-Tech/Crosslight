using System;
using System.Collections.Generic;
using System.Text;

namespace Crosslight.Language.CIL.Nodes.Visitors
{
    public class VisitContext
    {
        public CILVisitOptions Options { get; set; }
        public VisitFactory VisitFactory { get; set; }
    }
}
