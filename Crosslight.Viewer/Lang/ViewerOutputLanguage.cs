using Crosslight.API.Lang;
using Crosslight.API.Nodes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Crosslight.Viewer.Lang
{
    public class ViewerOutputLanguage : OutputLanguage
    {
        public override string Name => "Viewer";

        public override LanguageOptions Options { get; protected set; }

        public override object Encode(Node rootNode)
        {
            throw new NotImplementedException();
        }
    }
}
