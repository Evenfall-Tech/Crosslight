using Crosslight.API.Nodes.Implementations.Expressions;
using Crosslight.API.Nodes.Interfaces;
using Crosslight.API.Util;

namespace Crosslight.API.Nodes.Implementations.Access
{
    public class VariableDeclarationNode : Node, IIdentifierProvider
    {
        public override string Type => nameof(VariableDeclarationNode);

        public string Identifier { get; }
        private readonly SyncedProperty<ExpressionNode, Node> initializer;
        public ExpressionNode Initializer
        {
            get => initializer.Value;
            set => initializer.Value = value;
        }
        // TODO: c# also has bracketed argument list
        // public unsafe fixed byte bs[7];
        public VariableDeclarationNode(string identifier)
        {
            Identifier = identifier;
            initializer = new SyncedProperty<ExpressionNode, Node>(Children);
        }

        public override string ToString()
        {
            return Type;
        }
    }
}
