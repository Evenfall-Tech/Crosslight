namespace Crosslight.Language.Viewer.ViewModels.Utils
{
    public struct NodeLayer
    {
        public int X { get; set; }
        public int Y { get; set; }
        public NodeLayer(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"({X};{Y})";
        }

        public NodeLayer Clone()
        {
            return new NodeLayer(X, Y);
        }
    }
}
