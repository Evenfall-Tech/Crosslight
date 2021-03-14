using ICSharpCode.Decompiler.CSharp.Syntax;
using System.IO;
using System.Linq;

namespace Crosslight.Language.CIL.Util.ILSpy
{
    public static class AstNodeExtensions
    {
        public static string GetAssemblyTitle(this SyntaxTree tree)
        {
            var attributeSections = tree.Children
                .OfType<AttributeSection>()
                .Where(s => s.AttributeTarget == "assembly");
            var attribute = attributeSections
                .SelectMany(s => s.Attributes)
                .SingleOrDefault(a => a.Type.ToString() == "AssemblyTitle");
            if (attribute != null)
            {
                return attribute.Arguments
                    .OfType<PrimitiveExpression>()
                    .SingleOrDefault()
                    .Value.ToString();
            }
            else
            {
                return Path.GetFileNameWithoutExtension(tree.FileName);
            }
        }
    }
}
