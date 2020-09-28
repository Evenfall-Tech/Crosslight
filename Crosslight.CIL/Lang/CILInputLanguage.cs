using Crosslight.API.IO;
using Crosslight.API.Lang;
using Crosslight.API.Nodes;
using Crosslight.API.Nodes.Componentization;
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

            CILVisitOptions visitOptions = new CILVisitOptions();

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
                    new CILVisitOptions(visitOptions)
                    {
                        ModuleName = filePath,
                        ProjectName = decompiler.TypeSystem.MainModule.AssemblyName,
                    }
                )));
            }
            // TODO: split option-based logic into different methods.
            if (visitOptions.MergeProjectsWithSameName)
            {
                var uniqueProjects = nodes.OfType<ProjectNode>().GroupBy(e => e.Name);
                List<ProjectNode> toRemove = new List<ProjectNode>();
                if (uniqueProjects.Count() != nodes.Count)
                {
                    foreach (var group in uniqueProjects)
                    {
                        ProjectNode proj = group.First();
                        foreach (ProjectNode p in group)
                        {
                            if (p == proj) continue;
                            foreach (var nd in p.Attributes)
                                proj.Attributes.Add(nd);
                            foreach (var nd in p.Modules)
                                proj.Modules.Add(nd);
                            foreach (var nd in p.Children.Except(p.Attributes).Except(p.Modules))
                                proj.Children.Add(nd);
                            toRemove.Add(p);
                        }
                    }
                }
                foreach (var node in toRemove) nodes.Remove(node);
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
