using Crosslight.API.Nodes.Implementations.Expressions;
using Crosslight.API.Nodes.Interfaces;
using Crosslight.API.Nodes.Interfaces.Access;
using Crosslight.API.Nodes.Interfaces.Access.Modifiers;
using Crosslight.API.Util;

namespace Crosslight.API.Nodes.Implementations.Entities.Methods
{
    /// <summary>
    /// <see cref="ParameterNode"/> represents parameters of the function.
    /// </summary>
    public class ParameterNode : BaseParameterNode, IAttributesProvider, IModifiersProvider, IIdentifierProvider
    {
        public override string Type => nameof(ParameterNode);
        public string Identifier { get; }
        private readonly SyncedProperty<ExpressionNode, Node> defaultValue;
        public ExpressionNode DefaultValue
        {
            get => defaultValue.Value;
            set => defaultValue.Value = value;
        }
        public ParameterNode(string identifier)
        {
            Identifier = identifier;
            defaultValue = new SyncedProperty<ExpressionNode, Node>(Children);
        }
        public override string ToString()
        {
            return Type;
        }
    }
}
