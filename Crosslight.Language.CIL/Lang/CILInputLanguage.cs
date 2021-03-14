using Crosslight.API.IO.FileSystem;
using Crosslight.API.IO.FileSystem.Abstractions;
using Crosslight.API.Lang;
using Crosslight.API.Nodes;
using Crosslight.API.Nodes.Componentization;
using Crosslight.Language.CIL.Nodes;
using Crosslight.Language.CIL.Nodes.Visitors;
using ICSharpCode.Decompiler;
using ICSharpCode.Decompiler.CSharp;
using ICSharpCode.Decompiler.CSharp.Syntax;
using ICSharpCode.Decompiler.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Crosslight.Language.CIL.Lang
{
    public class CILInputLanguage : ILanguage
    {
        public string Name => "CIL";
        public LanguageType LanguageType => LanguageType.Input;

        private CILVisitOptions options;
        public LanguageConfig Config { get; protected set; }
        public ILanguageOptions Options
        {
            get => options;
            set
            {
                options = value as CILVisitOptions;
            }
        }

        public void LoadOptionsFromConfig(LanguageConfig config)
        {
            throw new NotImplementedException();
        }

        public CILInputLanguage()
        {
            options = new CILVisitOptions();
        }

        public IFileSystemItem Translate(IFileSystemItem source)
        {
            return ParseSource(source, null);
        }

        private IFileSystemItem ParseSource(IFileSystemItem source, IDirectory parent = null)
        {
            if (source is IDirectory directory)
            {
                if (directory.Items.Count > 0)
                {
                    var resultingDirectory = FileSystem.CreateFileSystemCollection(directory.Name, parent);
                    var items = directory.Items
                        .Select(x => ParseSource(x, resultingDirectory));
                    foreach (var item in items)
                    {
                        resultingDirectory.Items.Add(item);
                    }
                    return resultingDirectory;
                }
                else return FileSystem.CreateFileSystemCollection(directory.Name, parent);
            }
            else if (source is IStringFile stringFile)
            {
                return ParseStringFile(stringFile, parent);
            }
            else if (source is IPhysicalFile physicalFile)
            {
                return ParsePhysicalFile(physicalFile, parent);
            }
            else throw new ArgumentException($"{source.GetType().Name} is not supported in {Name}.");
        }

        private IFileSystemItem ParseStringFile(IStringFile source, IDirectory parent = null)
        {
            throw new NotImplementedException($"{source.GetType().Name} is not supported in {Name}.");
        }

        private IFileSystemItem ParsePhysicalFile(IPhysicalFile source, IDirectory parent = null)
        {
            string path = source.Path;
            CSharpDecompiler decompiler = GetDecompiler(path);
            SyntaxTree tree = decompiler.DecompileWholeModuleAsSingleFile();

            // TODO: add option loading
            // TODO: parse decompiler.TypeSystem.ReferencedModules for referenced modules.
            return FileSystem.CreateCustomFile(
                source.Path,
                tree.AcceptVisitor(new CILAstVisitor(
                    new CILVisitOptions(options)
                    {
                        ModuleName = path,
                        ProjectName = decompiler.TypeSystem.MainModule.AssemblyName,
                    }
                )),
                parent
            );
        }

        #region Option Methods
        // TODO: pull this method into an intermediate language.
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
