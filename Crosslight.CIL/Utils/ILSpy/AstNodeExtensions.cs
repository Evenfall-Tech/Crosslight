﻿using ICSharpCode.Decompiler.CSharp.Syntax;
using System.Linq;

namespace Crosslight.CIL.Utils.ILSpy
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
            return attribute.Arguments
                .OfType<PrimitiveExpression>()
                .SingleOrDefault()
                .Value.ToString();
        }
    }
}