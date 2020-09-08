using System;
using System.Collections.Generic;
using System.Text;

namespace Crosslight.CIL.Nodes.Visitors
{
    public class VisitOptions : ICloneable
    {
        public bool CreateProject { get; set; }
        public string ModuleName { get; set; }

        public VisitOptions()
        {
            // TODO: fill this class with options.
            CreateProject = true;
            ModuleName = DefaultModuleName;
        }

        public VisitOptions(VisitOptions other)
        {
            throw new NotImplementedException();
        }

        public object Clone()
        {
            return new VisitOptions(this);
        }

        public const string DefaultModuleName = "Module";
    }
}
