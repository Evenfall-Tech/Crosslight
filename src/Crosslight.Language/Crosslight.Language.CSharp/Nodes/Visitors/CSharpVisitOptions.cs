using Crosslight.API.Lang;
using System;

namespace Crosslight.Language.CSharp.Nodes.Visitors
{
    public enum CSharpSaveLocation
    {
        None,
        MultiFileSource,
        MultiStringSource,
    }
    public class CSharpVisitOptions : ILanguageOptions
    {
        public CSharpSaveLocation SaveLocation { get; set; }
        public string SaveDirectory { get; set; }
        public CSharpVisitOptions()
        {
            SaveLocation = CSharpSaveLocation.MultiStringSource;
            SaveDirectory = "./";
        }

        public CSharpVisitOptions(CSharpVisitOptions other)
        {
            SaveLocation = other.SaveLocation;
            SaveDirectory = other.SaveDirectory;
        }

        public object Clone()
        {
            return new CSharpVisitOptions(this);
        }
    }
}
