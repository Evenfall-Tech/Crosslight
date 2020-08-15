using Crosslight.Viewer.Models.Graph;
using Crosslight.Viewer.ViewModels.Utils;
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

        public GraphViewModel(GraphModel model)
        {
            this.model = model;
            Nodes = new ObservableViewModelCollection<NodeViewModel, NodeModel>(model.Nodes, new NodeViewModelFactory());
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
            Dictionary<NodeViewModel, Tuple<int, int>> nodeToLayer = new Dictionary<NodeViewModel, Tuple<int, int>>();
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
            [DisallowNull] Dictionary<NodeViewModel, Tuple<int, int>> nodeToLayer,
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
                nodeKVP.Key.Left = layersX[nodeKVP.Value.Item1];
                nodeKVP.Key.Top = layersY[nodeKVP.Value.Item2];
            }
        }

        /// <summary>
        /// Calculate position of layers based on the nodes that are there.
        /// </summary>
        /// <param name="nodeToLayer">Map of node-layer assignments.</param>
        /// <param name="layersX">Map of horizontal layer offsets.</param>
        /// <param name="layersY">Map of vertical layer offsets.</param>
        private void CalculateLayersPosition(
            [DisallowNull] Dictionary<NodeViewModel, Tuple<int, int>> nodeToLayer,
            [DisallowNull] Dictionary<int, double> layersX,
            [DisallowNull] Dictionary<int, double> layersY)
        {
            var uniqueLayers = nodeToLayer.Values.Distinct(new LayerEqualityComparer(true, true));
            var horizontalLayers = uniqueLayers.Distinct(new LayerEqualityComparer(true, false)).OrderBy(l => l.Item1).ToList();
            var verticalLayers = uniqueLayers.Distinct(new LayerEqualityComparer(false, true)).OrderBy(l => l.Item2).ToList();

            // Set horizontal layers.
            {
                // Find index of zeroth layer.
                int zeroIndex = horizontalLayers.FindIndex(match => match.Item1 == 0);
                double offset = layersX[0];
                // Set layer size iteratively backwards.
                for (int i = zeroIndex - 1; i >= 0; --i)
                {
                    int currentLayerPos = horizontalLayers[i].Item1;
                    double biggestNodeSize = nodeToLayer
                        .Where(e => e.Value.Item1 == currentLayerPos) // Find all nodes of this horizontal layer.
                        .Select(e => e.Key.Width)
                        .Max();
                    offset -= defOffsetX + biggestNodeSize;
                    layersX[currentLayerPos] = offset;
                }
                // Reset offset to iterate forwards.
                offset = layersX[0];

                for (int i = zeroIndex + 1; i < horizontalLayers.Count; ++i)
                {
                    int currentLayerPos = horizontalLayers[i].Item1;
                    double biggestNodeSize = nodeToLayer
                        .Where(e => e.Value.Item1 == currentLayerPos - 1) // Find all nodes of previous horizontal layer.
                        .Select(e => e.Key.Width)
                        .Max();
                    offset += biggestNodeSize + defOffsetX;
                    layersX[currentLayerPos] = offset;
                }
            }

            // Set vertical layers.
            {
                // Find index of zeroth layer.
                int zeroIndex = verticalLayers.FindIndex(match => match.Item2 == 0);
                double offset = layersY[0];
                // Set layer size iteratively backwards.
                for (int j = zeroIndex - 1; j >= 0; --j)
                {
                    int currentLayerPos = verticalLayers[j].Item2;
                    double biggestNodeSize = nodeToLayer
                        .Where(e => e.Value.Item2 == currentLayerPos) // Find all nodes of this vertical layer.
                        .Select(e => e.Key.Height)
                        .Max();
                    offset -= defOffsetY + biggestNodeSize;
                    layersY[currentLayerPos] = offset;
                }
                // Reset offset to iterate forwards.
                offset = layersY[0];

                for (int j = zeroIndex + 1; j < verticalLayers.Count; ++j)
                {
                    int currentLayerPos = verticalLayers[j].Item2;
                    double biggestNodeSize = nodeToLayer
                        .Where(e => e.Value.Item2 == currentLayerPos - 1) // Find all nodes of previous vertical layer.
                        .Select(e => e.Key.Height)
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
            [DisallowNull] Dictionary<NodeViewModel, Tuple<int, int>> nodeToLayer)
        {
            if (nodes == null || nodeToLayer == null) return;
            List<NodeViewModel> notVisitedNodes = new List<NodeViewModel>(nodes);
            Stack<Tuple<int, int>> currentLayer = new Stack<Tuple<int, int>>();
            currentLayer.Push(new Tuple<int, int>(0, 0));

            while (notVisitedNodes.Count > 0)
            {
                var node = notVisitedNodes[0];
                FillLayersForNodeAndRelatives(node, notVisitedNodes, nodeToLayer, currentLayer);
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
        private void FillLayersForNodeAndRelatives(
            [DisallowNull] NodeViewModel node,
            [DisallowNull] IList<NodeViewModel> notVisitedNodes,
            [DisallowNull] Dictionary<NodeViewModel, Tuple<int, int>> nodeToLayer,
            [DisallowNull] Stack<Tuple<int, int>> currentLayer)
        {
            nodeToLayer.Add(node, currentLayer.Peek());
            currentLayer.Push(GetNextLayer(currentLayer.Peek(), node.Direction));
            notVisitedNodes.Remove(node);

            var relatives = notVisitedNodes.Join(node.Connections, n => n.ID, i => i, (n, i) => n);
            foreach (var rel in relatives)
            {
                if (!nodeToLayer.ContainsKey(rel))
                {
                    FillLayersForNodeAndRelatives(rel, notVisitedNodes, nodeToLayer, currentLayer);
                }
            }

            currentLayer.Pop();
        }

        /// <summary>
        /// Get next layer index based on the direction provided.
        /// </summary>
        /// <param name="currentLayer">Current layer to use as an origin.</param>
        /// <param name="direction">Direction to move to.</param>
        private Tuple<int, int> GetNextLayer([DisallowNull] Tuple<int, int> currentLayer, [DisallowNull] GraphNodeDirection direction)
        {
            return direction switch
            {
                GraphNodeDirection.Down => new Tuple<int, int>(currentLayer.Item1, currentLayer.Item2 + 1),
                GraphNodeDirection.Right => new Tuple<int, int>(currentLayer.Item1 + 1, currentLayer.Item2),
                GraphNodeDirection.DownRight => new Tuple<int, int>(currentLayer.Item1 + 1, currentLayer.Item2 + 1),
                _ => throw new NotImplementedException(),
            };
        }
        /// <summary>
        /// A helper class to compare node layers of a 2d garph.
        /// </summary>
        private class LayerEqualityComparer : IEqualityComparer<Tuple<int, int>>
        {
            private readonly bool compareHorizontal;
            private readonly bool compareVertical;

            public LayerEqualityComparer(bool compareHorizontal, bool compareVertical)
            {
                this.compareHorizontal = compareHorizontal;
                this.compareVertical = compareVertical;
            }
            public bool Equals([AllowNull] Tuple<int, int> x, [AllowNull] Tuple<int, int> y)
            {
                if (x == null && y == null) return true;
                if (x == null || y == null) return false;
                bool eq = true;
                if (compareHorizontal) eq = eq && x.Item1 == y.Item1;
                if (compareVertical) eq = eq && x.Item2 == y.Item2;
                return eq;
            }

            public int GetHashCode([DisallowNull] Tuple<int, int> obj)
            {
                return (obj.Item1 ^ obj.Item2).GetHashCode();
            }
        }
    }
}
