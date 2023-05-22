using Crosslight.API.Nodes.Implementations.Access;
using Crosslight.API.Nodes.Interfaces;
using Crosslight.API.Nodes.Interfaces.Access;
using Crosslight.API.Util;

namespace Crosslight.API.Nodes.Implementations.Entities.Generics
{
    public enum TemplateTypeParameterVariance
    {
        Ordinary,
        In,
        Out,
    }
    /// <summary>
    /// <see cref="TemplateTypeParameterNode"/> represents
    /// template type paremeter T for generics.
    /// Supports [in|out] variance.
    /// </summary>
    public class TemplateTypeParameterNode : Node, IAttributesProvider, IIdentifierProvider
    {
        public override string Type => nameof(TemplateTypeParameterNode);
        public TemplateTypeParameterVariance Variance { get; protected set; }
        public SyncedList<AttributeNode, Node> Attributes { get; protected set; }
        public string Identifier { get; }

        public TemplateTypeParameterNode(string identifier, TemplateTypeParameterVariance variance)
        {
            Identifier = identifier;
            Variance = variance;
            Attributes = new SyncedList<AttributeNode, Node>(Children);
        }
        public override string ToString()
        {
            return Type;
        }
    }
}
