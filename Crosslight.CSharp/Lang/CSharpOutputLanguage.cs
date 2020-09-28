using Crosslight.API.Lang;
using Crosslight.API.Nodes;

namespace Crosslight.CSharp.Lang
{
    public class CSharpOutputLanguage : OutputLanguage
    {
        public override string Name => "CSharp";

        public override LanguageOptions Options { get; protected set; }

        public override object Encode(Node rootNode)
        {
            throw new System.NotImplementedException();
        }
    }
}
