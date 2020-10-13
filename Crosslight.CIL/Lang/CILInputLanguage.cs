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

        private CILVisitOptions options;
        private List<TransformNodes> transforms;
        // TODO: consider adding transforms on the API level.
        private delegate void TransformNodes(List<Node> nodes);
        public override LanguageConfig Config { get; protected set; }
        public override ILanguageOptions Options
        {
            get => options;
            set
            {
                options = value as CILVisitOptions;
            }
        }
        public override void LoadOptionsFromConfig(LanguageConfig config)
        {
            throw new NotImplementedException();
        }

        public CILInputLanguage()
        {
            options = new CILVisitOptions();
            transforms = new List<TransformNodes>()
            {
                OptionMergeProjectsWithSameName
            };
        }

        public override Node Decode(Source source)
        {
            List<Node> nodes = new List<Node>();
            ParseSource(source, nodes);
            // Apply transforms.
            foreach (var transform in transforms)
            {
                transform(nodes);
            }
            // Combine nodes under a root node parent.
            if (nodes.Count > 1)
            {
                RootNode rootNode = new RootNode();
                foreach (var n in nodes) rootNode.Children.Add(n);
                return rootNode;
            }
            else return nodes.First();
        }

        private Node ParseSource(Source source, List<Node> sink)
        {
            if (source is CompositeSource composite)
            {
                if (composite.Count > 0)
                {
                    return composite.Sources.Select(x => ParseSource(x, sink)).LastOrDefault();
                }
            }
            else if (source is MultiStringSource strings)
            {
                if (strings.Count > 0)
                {
                    return ParseStrings(strings.Strings, sink);
                }
            }
            else if (source is MultiFileSource files)
            {
                if (files.Count > 0)
                {
                    return ParseFiles(files.Files, sink);
                }
            }
            else throw new ArgumentException($"{source.GetType().Name} is not supported in {Name}.");
            return null;
        }

        private Node ParseStrings(IEnumerable<string> strings, List<Node> sink)
        {
            var res = strings.Select(s => ParseString(s));
            sink.AddRange(res);
            return res.FirstOrDefault();
        }

        private Node ParseString(string str)
        {
            throw new NotImplementedException($"String sources are not supported in {Name}.");
        }

        private Node ParseFiles(IEnumerable<string> paths, List<Node> sink)
        {
            var res = paths.Select(s => ParseFile(s));
            sink.AddRange(res);
            return res.FirstOrDefault();
        }

        private Node ParseFile(string path)
        {
            CSharpDecompiler decompiler = GetDecompiler(path);
            SyntaxTree tree = decompiler.DecompileWholeModuleAsSingleFile();

            // TODO: add option loading
            // TODO: parse decompiler.TypeSystem.ReferencedModules for referenced modules.
            return tree.AcceptVisitor(new CILAstVisitor(
                new CILVisitOptions(options)
                {
                    ModuleName = path,
                    ProjectName = decompiler.TypeSystem.MainModule.AssemblyName,
                }
            ));
        }

        #region Option Methods
        private void OptionMergeProjectsWithSameName(List<Node> nodes)
        {
            if (options.MergeProjectsWithSameName)
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
        }
        #endregion

        #region Decompiler
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
        #endregion
    }
}
