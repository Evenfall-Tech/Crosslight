namespace Crosslight.API.Nodes.Implementations
{
    public class TokenNode : Node
    {
        public override string Type => nameof(TokenNode);
        public string Token { get; protected set; }
        public TokenNode(string token)
        {
            Token = token;
        }
    }
}
