using Crosslight.API.Lang;
using System;

namespace Crosslight.Language.CSharp.Nodes.Visitors
{
    public class CSharpVisitOptions : ILanguageOptions
    {
        public CSharpVisitOptions()
        {
        }

        public CSharpVisitOptions(CSharpVisitOptions other)
        {
        }

        public object Clone()
        {
            return new CSharpVisitOptions(this);
        }
    }
}
