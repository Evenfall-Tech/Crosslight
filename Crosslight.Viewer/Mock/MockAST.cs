using Crosslight.API.Nodes;
using Crosslight.Viewer.Nodes;
using System;

namespace Crosslight.Viewer.Mock
{
    public static class MockAST
    {
        public static Node CreateAST()
        {
            Random r = new Random(42);
            ViewerNode result = CreateNode(r.Next(5), r);
            return result;
        }

        private static ViewerNode CreateNode(int childrenCount, Random random)
        {
            ViewerNode node = new ViewerNode();
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
    }
}
