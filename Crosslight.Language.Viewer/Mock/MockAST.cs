using Crosslight.API.IO.FileSystem;
using Crosslight.API.IO.FileSystem.Abstractions;
using Crosslight.API.Lang;
using Crosslight.API.Nodes;
using Crosslight.Language.Viewer.Nodes;
using Crosslight.Language.Viewer.Nodes.Visitors;
using System;

namespace Crosslight.Language.Viewer.Mock
{
    public class MockAST : ILanguage
    {
        public string Name => "Mock";
        public LanguageType LanguageType => LanguageType.Input;

        private ViewerOptions options;
        public LanguageConfig Config { get; protected set; }
        public ILanguageOptions Options
        {
            get => options;
            set
            {
                options = value as ViewerOptions;
            }
        }
        public void LoadOptionsFromConfig(LanguageConfig config)
        {
            throw new NotImplementedException();
        }

        public MockAST()
        {
            options = new ViewerOptions();
        }

        public static ViewerNode CreateAST()
        {
            Random r = new Random(42);
            ViewerNode result = CreateNode(r.Next(10), r);
            return result;
        }

        private static ViewerNode CreateNode(int childrenCount, Random random)
        {
            ViewerNode node = new ViewerNode(null);
            if (childrenCount > 0)
            {
                var children = new ViewerNode[childrenCount];
                for (int i = 0; i < childrenCount; ++i)
                {
                    children[i] = CreateNode(random.Next(childrenCount), random);
                    children[i].SetParent(node);
                }
                node.SetChildren(children);
            }
            return node;
        }

        public IFileSystemItem Translate(IFileSystemItem source)
        {
            return FileSystem.CreateCustomFile(source?.Name ?? "AST", CreateAST(), null);
        }
    }
}
