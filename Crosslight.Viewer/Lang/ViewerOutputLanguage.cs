using Crosslight.API.Lang;
using Crosslight.API.Nodes;
using Crosslight.Viewer.Nodes.Visitors;
using System;

namespace Crosslight.Viewer.Lang
{
    public class ViewerOutputLanguage : OutputLanguage
    {
        public override string Name => "Viewer";

        public override LanguageOptions Options { get; protected set; }

        public override object Encode(Node rootNode)
        {
            VisitOptions visitOptions = new VisitOptions();
            throw new NotImplementedException();
        }
    }
}
