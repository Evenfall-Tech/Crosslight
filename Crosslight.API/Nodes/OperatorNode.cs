namespace Crosslight.API.Nodes
{
    public class OperatorNode : Node
    {
        public override string Type => nameof(OperatorNode);
        public string Token { get; protected set; }
        public OperatorNode(string token)
        {
            Token = token;
        }
    }
}
