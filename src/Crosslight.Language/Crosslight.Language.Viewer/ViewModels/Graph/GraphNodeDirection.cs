namespace Crosslight.Language.Viewer.ViewModels.Graph
{
    public struct GraphNodeDirection
    {
        public GraphNodeAlignment Horizontal { get; set; }
        public GraphNodeAlignment Vertical { get; set; }

        public static GraphNodeDirection Right => new GraphNodeDirection()
        {
            Horizontal = GraphNodeAlignment.Highest,
            Vertical = GraphNodeAlignment.Middle,
        };

        public static GraphNodeDirection DownRight => new GraphNodeDirection()
        {
            Horizontal = GraphNodeAlignment.Highest,
            Vertical = GraphNodeAlignment.Highest,
        };

        public static GraphNodeDirection Down => new GraphNodeDirection()
        {
            Horizontal = GraphNodeAlignment.Middle,
            Vertical = GraphNodeAlignment.Highest,
        };

        public static GraphNodeDirection DownLeft => new GraphNodeDirection()
        {
            Horizontal = GraphNodeAlignment.Lowest,
            Vertical = GraphNodeAlignment.Highest,
        };

        public static GraphNodeDirection Left => new GraphNodeDirection()
        {
            Horizontal = GraphNodeAlignment.Lowest,
            Vertical = GraphNodeAlignment.Middle,
        };

        public static GraphNodeDirection UpLeft => new GraphNodeDirection()
        {
            Horizontal = GraphNodeAlignment.Lowest,
            Vertical = GraphNodeAlignment.Lowest,
        };

        public static GraphNodeDirection Up => new GraphNodeDirection()
        {
            Horizontal = GraphNodeAlignment.Middle,
            Vertical = GraphNodeAlignment.Lowest,
        };

        public static GraphNodeDirection UpRight => new GraphNodeDirection()
        {
            Horizontal = GraphNodeAlignment.Highest,
            Vertical = GraphNodeAlignment.Lowest,
        };
    }
}
