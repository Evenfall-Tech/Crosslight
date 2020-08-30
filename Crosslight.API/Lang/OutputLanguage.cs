using Crosslight.API.Nodes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Crosslight.API.Lang
{
    public abstract class OutputLanguage
    {
        // TODO: replace object with a meaningful type
        public abstract object Encode(Node rootNode);
        public abstract string Name { get; }
    }
}
