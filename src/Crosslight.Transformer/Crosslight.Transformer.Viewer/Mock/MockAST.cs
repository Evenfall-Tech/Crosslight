using Crosslight.API.IO.FileSystem;
using Crosslight.API.IO.FileSystem.Abstractions;
using Crosslight.API.Transformers;
using Crosslight.Transformer.Viewer.Nodes;
using Crosslight.Transformer.Viewer.Nodes.Visitors;
using System;

namespace Crosslight.Transformer.Viewer.Mock
{
    public class MockAST : ITransformer
    {
        public string Name => "Mock";
        public TransformerType TransformerType => TransformerType.Input;

        private ViewerOptions options;
        public TransformerConfig Config { get; protected set; }
        public ITransformerOptions Options
        {
            get => options;
            set
            {
                options = value as ViewerOptions;
            }
        }
        public void LoadOptionsFromConfig(TransformerConfig config)
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
