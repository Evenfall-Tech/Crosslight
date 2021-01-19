using Crosslight.API.IO.FileSystem;
using Crosslight.API.IO.FileSystem.Abstractions;
using Crosslight.API.Lang;
using Crosslight.API.Nodes;
using Crosslight.Viewer.Nodes;
using Crosslight.Viewer.Nodes.Visitors;
using System;

namespace Crosslight.Viewer.Mock
{
    public class MockAST : InputLanguage
    {
        public override string Name => "Mock";

        private ViewerOptions options;
        public override LanguageConfig Config { get; protected set; }
        public override ILanguageOptions Options
        {
            get => options;
            set
            {
                options = value as ViewerOptions;
            }
        }
        public override void LoadOptionsFromConfig(LanguageConfig config)
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

        public override IFileSystemItem Decode(IFileSystemItem source)
        {
            return FileSystem.CreateCustomFile(source?.Name ?? "AST", CreateAST(), null);
        }
    }
}
