using Crosslight.API.IO;
using Crosslight.API.Nodes;
using System;

namespace Crosslight.API.Lang
{
    public abstract class InputLanguage
    {
        // TODO: replace Node return type with a more specialized root node type
        public abstract Node Decode(Source source);
    }
}
