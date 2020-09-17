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
using System.Collections.Generic;
using System.Linq;

namespace Crosslight.CIL.Lang
{
    public class CILInputLanguage : InputLanguage
    {
        public override string Name => "CIL";

        public override LanguageOptions Options { get; protected set; }

        public override Node Decode(Source source)
        {
            if (!(source is MultiFileSource fileSource))
            {
                // TODO: add logging messages all around
                // TODO: add loading from string data if possible
                throw new ArgumentException($"No input found in source.");
            }

            VisitOptions visitOptions = new VisitOptions()
            {
                CreateProject = true,
                SplitNamespaces = false,
                FullModulePath = false,
            };

            // TODO: allow multiple files.
            if (fileSource.Count == 0)
            {
                throw new ArgumentException($"No input found in source.");
            }
            List<Node> nodes = new List<Node>();
            foreach (string filePath in fileSource.Files)
            {
                CSharpDecompiler decompiler = GetDecompiler(filePath);
                SyntaxTree tree = decompiler.DecompileWholeModuleAsSingleFile();

                // TODO: add option loading
                // TODO: parse decompiler.TypeSystem.ReferencedModules for referenced modules.
                nodes.Add(tree.AcceptVisitor(new CILAstVisitor(
                    new VisitOptions(visitOptions)
                    {
                        ModuleName = filePath,
                        ProjectName = decompiler.TypeSystem.MainModule.AssemblyName,
                    }
                )));
            }
            if (nodes.Count > 1)
            {
                RootNode rootNode = new RootNode();
                foreach (var n in nodes) rootNode.Children.Add(n);
                return rootNode;
            }
            else return nodes.First();
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
