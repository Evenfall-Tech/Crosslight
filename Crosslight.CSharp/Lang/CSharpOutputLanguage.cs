using Crosslight.API.Lang;
using Crosslight.API.Nodes;
using Crosslight.CSharp.Nodes.Visitors;
using System;

namespace Crosslight.CSharp.Lang
{
    public class CSharpOutputLanguage : OutputLanguage
    {
        public override string Name => "CSharp";

        private CSharpVisitOptions options;
        public override LanguageConfig Config { get; protected set; }
        public override ILanguageOptions Options
        {
            get => options;
            set
            {
                options = value as CSharpVisitOptions;
            }
        }
        public override void LoadOptionsFromConfig(LanguageConfig config)
        {
            throw new NotImplementedException();
        }

        public CSharpOutputLanguage()
        {
            options = new CSharpVisitOptions();
        }

        public override object Encode(Node rootNode)
        {
            throw new System.NotImplementedException();
        }
    }
}
