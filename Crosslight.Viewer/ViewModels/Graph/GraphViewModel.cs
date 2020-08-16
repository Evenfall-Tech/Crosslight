using Crosslight.Viewer.Models.Graph;
using Crosslight.Viewer.ViewModels.Utils;
using Crosslight.Viewer.Views.Utils;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading;

namespace Crosslight.Viewer.ViewModels.Graph
{
    public class GraphViewModel : ViewModelBase, IViewModelFor<GraphModel>
    {
        private const double defOffsetX = 20.0, defOffsetY = 20.0;
        private GraphModel model;

        public GraphViewModel(GraphModel model, GraphNodeDirection graphNodeDirection)
        {
            this.model = model;
            Nodes = new ObservableViewModelCollection<NodeViewModel, NodeModel>
                (model.Nodes, new NodeViewModelFactory(graphNodeDirection));
            UpdateNodesSize();
        }

        public GraphModel Model
        {
            get => model;
            set
            {
                model = value;
                OnPropertyChanged("Model");
            }
        }

        public ObservableViewModelCollection<NodeViewModel, NodeModel> Nodes { get; set; }

        public bool IsViewModelOf(GraphModel model)
        {
            return this.model == model;
        }

        /// <summary>
        /// Sort all the nodes, setting their position.
        /// This method is very resource-heavy, 
        /// so should be called as rarely as possible.
        /// </summary>
        public void Sort(GraphNodeAlignment horizontalAlignment, GraphNodeAlignment verticalAlignment)
        {
            Dictionary<int, double> layersX = new Dictionary<int, double>();
            Dictionary<int, double> layersY = new Dictionary<int, double>();
            Dictionary<NodeViewModel, NodeLayer> nodeToLayer = new Dictionary<NodeViewModel, NodeLayer>();
            layersX[0] = defOffsetX / 2.0;
            layersY[0] = defOffsetY / 2.0;

            // Assign all nodes to layers.
            FillLayersForNodes(Nodes, nodeToLayer);

            //TODO: check for overlaps and shift accordingly.
            //FixNodeOverlaps(nodes, nodeToLayer);

            // Calculate layer size and offset based on nodes that it has.
            CalculateLayersPosition(nodeToLayer, layersX, layersY);

            PlaceNodesInsideLayers(nodeToLayer, layersX, layersY, horizontalAlignment, verticalAlignment);
        }

        /// <summary>
        /// Set nodes position based on layer assignments.
        /// </summary>
        /// <param name="nodeToLayer">Map of node-layer assignments.</param>
        /// <param name="layersX">Map of horizontal layer offsets.</param>
        /// <param name="layersY">Map of vertical layer offsets.</param>
        /// <param name="horizontalAlignment">Horizontal node alignment inside a layer.</param>
        /// <param name="verticalAlignment">Vertical node alignment inside a layer.</param>
        private void PlaceNodesInsideLayers(
            [DisallowNull] Dictionary<NodeViewModel, NodeLayer> nodeToLayer,
            [DisallowNull] Dictionary<int, double> layersX,
            [DisallowNull] Dictionary<int, double> layersY,
            [DisallowNull] GraphNodeAlignment horizontalAlignment,
            [DisallowNull] GraphNodeAlignment verticalAlignment)
        {
            if (horizontalAlignment != GraphNodeAlignment.Lowest || verticalAlignment != GraphNodeAlignment.Lowest)
                throw new NotImplementedException();
            //TODO: finish other alignments, but they require layer size recalculation for edge layers. Others are nextLayer-offset-curLayer

            foreach (var nodeKVP in nodeToLayer)
            {
                nodeKVP.Key.Left = layersX[nodeKVP.Value.X];
                nodeKVP.Key.Top = layersY[nodeKVP.Value.Y];
            }
        }

        /// <summary>
        /// Calculate position of layers based on the nodes that are there.
        /// </summary>
        /// <param name="nodeToLayer">Map of node-layer assignments.</param>
        /// <param name="layersX">Map of horizontal layer offsets.</param>
        /// <param name="layersY">Map of vertical layer offsets.</param>
        private void CalculateLayersPosition(
            [DisallowNull] Dictionary<NodeViewModel, NodeLayer> nodeToLayer,
            [DisallowNull] Dictionary<int, double> layersX,
            [DisallowNull] Dictionary<int, double> layersY)
        {
            var uniqueLayers = nodeToLayer.Values.Distinct(new LayerEqualityComparer(true, true));
            var horizontalLayers = uniqueLayers.Distinct(new LayerEqualityComparer(true, false)).OrderBy(l => l.X).ToList();
            var verticalLayers = uniqueLayers.Distinct(new LayerEqualityComparer(false, true)).OrderBy(l => l.Y).ToList();

            // Set horizontal layers.
            {
                // Find index of zeroth layer.
                int zeroIndex = horizontalLayers.FindIndex(match => match.X == 0);
                double offset = layersX[0];
                // Set layer size iteratively backwards.
                for (int i = zeroIndex - 1; i >= 0; --i)
                {
                    int currentLayerPos = horizontalLayers[i].X;
                    double biggestNodeSize = nodeToLayer
                        .Where(e => e.Value.X == currentLayerPos) // Find all nodes of this horizontal layer.
                        .Select(e => e.Key.Width)
                        .DefaultIfEmpty(0.0)
                        .Max();
                    offset -= defOffsetX + biggestNodeSize;
                    layersX[currentLayerPos] = offset;
                }
                // Reset offset to iterate forwards.
                offset = layersX[0];

                for (int i = zeroIndex + 1; i < horizontalLayers.Count; ++i)
                {
                    int currentLayerPos = horizontalLayers[i].X;
                    double biggestNodeSize = nodeToLayer
                        .Where(e => e.Value.X == horizontalLayers[i - 1].X) // Find all nodes of previous horizontal layer.
                        .Select(e => e.Key.Width)
                        .DefaultIfEmpty(0.0)
                        .Max();
                    offset += biggestNodeSize + defOffsetX;
                    layersX[currentLayerPos] = offset;
                }
            }

            // Set vertical layers.
            {
                // Find index of zeroth layer.
                int zeroIndex = verticalLayers.FindIndex(match => match.Y == 0);
                double offset = layersY[0];
                // Set layer size iteratively backwards.
                for (int j = zeroIndex - 1; j >= 0; --j)
                {
                    int currentLayerPos = verticalLayers[j].Y;
                    double biggestNodeSize = nodeToLayer
                        .Where(e => e.Value.Y == currentLayerPos) // Find all nodes of this vertical layer.
                        .Select(e => e.Key.Height)
                        .DefaultIfEmpty(0.0)
                        .Max();
                    offset -= defOffsetY + biggestNodeSize;
                    layersY[currentLayerPos] = offset;
                }
                // Reset offset to iterate forwards.
                offset = layersY[0];

                for (int j = zeroIndex + 1; j < verticalLayers.Count; ++j)
                {
                    int currentLayerPos = verticalLayers[j].Y;
                    double biggestNodeSize = nodeToLayer
                        .Where(e => e.Value.Y == verticalLayers[j - 1].Y) // Find all nodes of previous vertical layer.
                        .Select(e => e.Key.Height)
                        .DefaultIfEmpty(0.0)
                        .Max();
                    offset += biggestNodeSize + defOffsetY;
                    layersY[currentLayerPos] = offset;
                }
            }
        }

        /// <summary>
        /// Fill layer info for node list.
        /// </summary>
        /// <param name="nodes">Node list to assign layers to.</param>
        /// <param name="nodeToLayer">Map of node-layer assignments.</param>
        private void FillLayersForNodes(
            [DisallowNull] IList<NodeViewModel> nodes,
            [DisallowNull] Dictionary<NodeViewModel, NodeLayer> nodeToLayer)
        {
            if (nodes == null || nodeToLayer == null) return;
            List<NodeViewModel> notVisitedNodes = new List<NodeViewModel>(nodes);
            Stack<NodeLayer> currentLayer = new Stack<NodeLayer>();
            currentLayer.Push(new NodeLayer(0, 0));
            int shift = 0;

            while (notVisitedNodes.Count > 0)
            {
                var node = notVisitedNodes[0];
                FillLayersForNodeAndRelatives(node, notVisitedNodes, nodeToLayer, currentLayer, ref shift);
            }

            if (nodes.Count != nodeToLayer.Count)
                throw new ArgumentOutOfRangeException(
                    $"Not all nodes were assigned to layers. {nodes.Count} nodes and {nodeToLayer.Count} assignees.");
        }

        /// <summary>
        /// Fill layer info for current node and its connections.
        /// </summary>
        /// <param name="node">Current node to process.</param>
        /// <param name="notVisitedNodes">List of not yet visited nodes.</param>
        /// <param name="nodeToLayer">Map of node-layer assignments.</param>
        /// <param name="currentLayer">Current layer stack.</param>
        /// <param name="shift">Vertical shift of element.</param>
        private void FillLayersForNodeAndRelatives(
            [DisallowNull] NodeViewModel node,
            [DisallowNull] IList<NodeViewModel> notVisitedNodes,
            [DisallowNull] Dictionary<NodeViewModel, NodeLayer> nodeToLayer,
            [DisallowNull] Stack<NodeLayer> currentLayer,
            ref int shift)
        {
            var oldLayer = currentLayer.Peek().Clone();
            oldLayer.Y += shift;
            nodeToLayer.Add(node, oldLayer);
            currentLayer.Push(GetNextLayer(currentLayer.Peek(), node.Direction));
            notVisitedNodes.Remove(node);

            var relatives = notVisitedNodes.Join(node.Connections, n => n.ID, i => i, (n, i) => n).ToArray();
            foreach (var rel in relatives)
            {
                if (!nodeToLayer.ContainsKey(rel))
                {
                    FillLayersForNodeAndRelatives(rel, notVisitedNodes, nodeToLayer, currentLayer, ref shift);
                    shift++;
                }
            }

            currentLayer.Pop();
        }

        /// <summary>
        /// Get next layer index based on the direction provided.
        /// </summary>
        /// <param name="currentLayer">Current layer to use as an origin.</param>
        /// <param name="direction">Direction to move to.</param>
        private NodeLayer GetNextLayer([DisallowNull] NodeLayer currentLayer, [DisallowNull] GraphNodeDirection direction)
        {
            int x = direction.Horizontal switch
            {
                GraphNodeAlignment.Lowest => currentLayer.X - 1,
                GraphNodeAlignment.Highest => currentLayer.X + 1,
                GraphNodeAlignment.Middle => currentLayer.X,
                _ => throw new NotImplementedException(),
            };
            int y = direction.Vertical switch
            {
                GraphNodeAlignment.Lowest => currentLayer.Y - 1,
                GraphNodeAlignment.Highest => currentLayer.Y + 1,
                GraphNodeAlignment.Middle => currentLayer.Y,
                _ => throw new NotImplementedException(),
            };
            return new NodeLayer(x, y);
        }
        /// <summary>
        /// A helper class to compare node layers of a 2d garph.
        /// </summary>
        private class LayerEqualityComparer : IEqualityComparer<NodeLayer>
        {
            private readonly bool compareHorizontal;
            private readonly bool compareVertical;

            public LayerEqualityComparer(bool compareHorizontal, bool compareVertical)
            {
                this.compareHorizontal = compareHorizontal;
                this.compareVertical = compareVertical;
            }
            public bool Equals(NodeLayer x, NodeLayer y)
            {
                bool eq = true;
                if (compareHorizontal) eq = eq && x.X == y.X;
                if (compareVertical) eq = eq && x.Y == y.Y;
                return eq;
            }

            public int GetHashCode([DisallowNull] NodeLayer obj)
            {
                int res = 0;
                if (compareHorizontal) res ^= obj.X;
                if (compareVertical) res ^= obj.Y;
                return res.GetHashCode();
            }
        }

        private void UpdateNodesSize()
        {
            foreach (var node in Nodes)
            {
                var size = GraphNodeControlBuilder.GetGraphControlSize(node);
                node.Width = size.Width;
                node.Height = size.Height;
            }
        }
    }
}
