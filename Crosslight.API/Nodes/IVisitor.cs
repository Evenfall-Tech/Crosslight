namespace Crosslight.API.Nodes
{
    public interface IVisitor
    {
        object Visit(Node node);
    }
    public interface IVisitor<out S>
    {
        S Visit(Node node);
    }
    public interface IVisitor<in T, out S>
    {
        S Visit(Node node, T data);
    }
}
