using Crosslight.API.IO;
using Crosslight.API.Lang;
using Crosslight.API.Nodes;
using Crosslight.CIL.Nodes;
using Crosslight.CIL.Nodes.Visitors;
using ICSharpCode.Decompiler;
using ICSharpCode.Decompiler.CSharp;
using ICSharpCode.Decompiler.CSharp.Syntax;
using ICSharpCode.Decompiler.Metadata;
using System;
using System.Linq;

namespace Crosslight.CIL.Lang
{
    public class CILInputLanguage : InputLanguage
    {
        public override string Name => "CIL";

        public override LanguageOptions Options { get; protected set; }

        public override Node Decode(Source source)
        {
            if (!source.HasFiles)
            {
                // TODO: add logging messages all around
                if (source.HasData)
                    throw new System.NotImplementedException($"{nameof(CILInputLanguage)} does not support loading direct data.");
                // TODO: add loading from string data if possible
                throw new ArgumentException($"No input found in source.");
            }

            // TODO: allow multiple files.
            string file = source.Files.FirstOrDefault();
            CSharpDecompiler decompiler = GetDecompiler(file);
            SyntaxTree tree = decompiler.DecompileWholeModuleAsSingleFile();
            // TODO: add option loading
            return tree.AcceptVisitor(new CILAstVisitor(
                new VisitOptions()
                {
                    ModuleName = file,
                    CreateProject = true,
                    SplitNamespaces = false,
                    FullModulePath = false,
                }
            ));
        }
        // From ICSharpCode.Decompiler.Console
        CSharpDecompiler GetDecompiler(string assemblyFileName)
        {
            var module = new PEFile(assemblyFileName);
            var resolver = new UniversalAssemblyResolver(assemblyFileName, false, module.Reader.DetectTargetFrameworkId());
            //foreach (var path in ReferencePaths)
            //{
            //    resolver.AddSearchDirectory(path);
            //}
            return new CSharpDecompiler(assemblyFileName, resolver, GetSettings());
        }

        DecompilerSettings GetSettings()
        {
            return new DecompilerSettings(LanguageVersion.Latest)
            {
                ThrowOnAssemblyResolveErrors = false,
                RemoveDeadCode = false,
                RemoveDeadStores = false
            };
        }
    }
}
