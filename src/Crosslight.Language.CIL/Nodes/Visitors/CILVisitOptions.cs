using Crosslight.API.Lang;
using System;

namespace Crosslight.Language.CIL.Nodes.Visitors
{
    public class CILVisitOptions : ILanguageOptions
    {
        public bool CreateProject { get; set; }
        public bool SplitNamespaces { get; set; }
        public bool FullModulePath { get; set; }
        public bool MergeProjectsWithSameName { get; set; }
        public string ModuleName { get; set; }
        public string ProjectName { get; set; }

        public CILVisitOptions()
        {
            // TODO: fill this class with options.
            CreateProject = true;
            SplitNamespaces = false;
            FullModulePath = false;
            MergeProjectsWithSameName = true;
            ModuleName = DefaultProjectName;
            ProjectName = DefaultProjectName;
        }

        public CILVisitOptions(CILVisitOptions other)
        {
            CreateProject = other.CreateProject;
            SplitNamespaces = other.SplitNamespaces;
            FullModulePath = other.FullModulePath;
            MergeProjectsWithSameName = other.MergeProjectsWithSameName;
            ModuleName = other.ModuleName;
            ProjectName = other.ProjectName;
        }

        public object Clone()
        {
            return new CILVisitOptions(this);
        }

        public const string DefaultProjectName = "Module";
    }
}
