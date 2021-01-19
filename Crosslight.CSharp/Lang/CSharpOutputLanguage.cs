using Crosslight.API.IO.FileSystem.Abstractions;
using Crosslight.API.Lang;
using Crosslight.API.Nodes;
using Crosslight.CSharp.Nodes.Visitors;
using System;

namespace Crosslight.CSharp.Lang
{
    public class CSharpOutputLanguage : ILanguage
    {
        public string Name => "CSharp";
        public LanguageType LanguageType => LanguageType.Output;

        private CSharpVisitOptions options;
        public LanguageConfig Config { get; protected set; }
        public ILanguageOptions Options
        {
            get => options;
            set
            {
                options = value as CSharpVisitOptions;
            }
        }
        public void LoadOptionsFromConfig(LanguageConfig config)
        {
            throw new NotImplementedException();
        }

        public IFileSystemItem Encode(IFileSystemItem rootNode)
        {
            throw new NotImplementedException();
        }

        public CSharpOutputLanguage()
        {
            options = new CSharpVisitOptions();
        }
    }
}
