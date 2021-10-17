using Crosslight.API.Util;

namespace Crosslight.API.Nodes.Implementations.Expressions.Declaration
{
    // TODO: add argument list
    public class VariableDeclaratorNode : Node
    {
        public override string Type => nameof(VariableDeclaratorNode);

        // TODO: replace with IdentifierNameNode or something later
        public string Identifier { get; set; }

        private readonly SyncedProperty<ExpressionNode, Node> initializer;

        public ExpressionNode Initializer
        {
            get => initializer.Value;
            protected set => initializer.Value = value;
        }

        public VariableDeclaratorNode()
        {
            initializer = new SyncedProperty<ExpressionNode, Node>(Children);
        }
    }
}
