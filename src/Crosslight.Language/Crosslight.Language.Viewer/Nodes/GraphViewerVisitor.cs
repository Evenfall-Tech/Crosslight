using Crosslight.API.Nodes;
using Crosslight.API.Nodes.Componentization;
using Crosslight.API.Nodes.Entities;
using Crosslight.Language.Viewer.Models.Graph;
using System.Collections.Generic;

namespace Crosslight.Language.Viewer.Nodes
{
    public class GraphViewerVisitor : IViewerVisitor
    {
        public GraphModel Context { get; private set; }
        private int idGen;

        public GraphViewerVisitor()
        {
            Context = new GraphModel
            {
                Nodes = new Dictionary<int, NodeModel>()
            };
            idGen = 0;
        }
        public object Visit(Node node)
        {
            string name = node.ToString();// + idGen.ToString();
            var parent = new NodeModel()
            {
                ID = idGen++,
                Data = name,
                Type = node.Type,
                Connections = new List<int>(),
            };
            Context.Nodes.Add(parent.ID, parent);
            if (node.Children != null)
            {
                foreach (Node nodeChild in node.Children)
                {
                    NodeModel visitResult;
                    if (nodeChild is ViewerNode viewerNode)
                    {
                        visitResult = Visit(viewerNode) as NodeModel;
                    }
                    else
                    {
                        visitResult = Visit(nodeChild) as NodeModel;
                    }
                    parent.Connections.Add(visitResult.ID);
                }
            }
            return parent;
        }

        public object Visit(ViewerNode node)
        {
            string name = (node.Content ?? node).ToString() + idGen.ToString();
            var parent = new NodeModel()
            {
                ID = idGen++,
                Data = name,
                Type = node.Type,
                Connections = new List<int>(),
            };
            Context.Nodes.Add(parent.ID, parent);
            if (node.Children != null)
            {
                foreach (Node nodeChild in node.Children)
                {
                    ViewerNode viewerNode;
                    if (nodeChild is ViewerNode)
                    {
                        viewerNode = nodeChild as ViewerNode;
                    }
                    else
                    {
                        viewerNode = new ViewerNode(nodeChild);
                    }
                    var child = Visit(viewerNode) as NodeModel;
                    parent.Connections.Add(child.ID);
                }
            }
            return parent;
        }

        object IViewerVisitor.Visit(ViewerNode node)
        {
            return Visit((Node)node);
        }

        object IVisitor.Visit(FunctionEntityNode node)
        {
            return Visit((Node)node);
        }

        object IVisitor.Visit(EnumNode node)
        {
            return Visit((Node)node);
        }

        object IVisitor.Visit(StructNode node)
        {
            return Visit((Node)node);
        }

        object IVisitor.Visit(InterfaceNode node)
        {
            return Visit((Node)node);
        }

        object IVisitor.Visit(ClassNode node)
        {
            return Visit((Node)node);
        }

        object IVisitor.Visit(NamespaceNode node)
        {
            return Visit((Node)node);
        }

        object IVisitor.Visit(ModuleNode node)
        {
            return Visit((Node)node);
        }

        object IVisitor.Visit(ProjectNode node)
        {
            return Visit((Node)node);
        }
    }
}
