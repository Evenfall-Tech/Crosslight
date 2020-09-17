using System;
using System.Collections.Generic;
using System.Text;

namespace Crosslight.CIL.Nodes.Visitors
{
    public class VisitOptions : ICloneable
    {
        public bool CreateProject { get; set; }
        public bool SplitNamespaces { get; set; }
        public bool FullModulePath { get; set; }
        public string ModuleName { get; set; }
        public string ProjectName { get; set; }

        public VisitOptions()
        {
            // TODO: fill this class with options.
            CreateProject = true;
            SplitNamespaces = false;
            FullModulePath = false;
            ModuleName = DefaultProjectName;
            ProjectName = DefaultProjectName;
        }

        public VisitOptions(VisitOptions other)
        {
            CreateProject = other.CreateProject;
            SplitNamespaces = other.SplitNamespaces;
            FullModulePath = other.FullModulePath;
            ModuleName = other.ModuleName;
            ProjectName = other.ProjectName;
        }

        public object Clone()
        {
            return new VisitOptions(this);
        }

        public const string DefaultProjectName = "Module";
    }
}
