namespace Crosslight.API.Nodes
{
    public interface IVisitor
    {
        object Visit(Node node);
    }
}
