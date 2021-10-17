using Crosslight.API.Nodes.Interfaces;
using Crosslight.API.Nodes.Interfaces.Access;
using Crosslight.API.Nodes.Interfaces.Access.Modifiers;
using Crosslight.API.Nodes.Interfaces.Entities;
using Crosslight.API.Util;
using System;

namespace Crosslight.API.Nodes.Implementations.Entities
{
    /// <summary>
    /// <see cref="FunctionEntityDeclarationNode"/> represents C# delegate declaration.
    /// </summary>
    public class FunctionEntityDeclarationNode : MemberDeclarationNode, IAttributesProvider, IModifiersProvider, IGenericDefinitionProvider, IIdentifierProvider
    {
        public override string Type => nameof(FunctionEntityDeclarationNode);
        public SyncedList<TemplateTypeParameterNode, Node> TypeParameters { get; protected set; }
        public SyncedList<TypeConstraintNode, Node> Constraints { get; protected set; }
        public int Arity { get; protected set; }
        public string Identifier { get; }

        public FunctionEntityDeclarationNode(string identifier)
        {
            Identifier = identifier;
            throw new NotImplementedException();
            // TODO: add FunctionType properties return type and parameter list.
        }
        public override string ToString()
        {
            return Type;
        }
        public override object AcceptVisitor(IVisitor visitor)
        {
            return visitor.Visit(this);
        }
        public override S AcceptVisitor<S>(IVisitor<S> visitor)
        {
            return visitor.Visit(this);
        }
        public override S AcceptVisitor<T, S>(IVisitor<T, S> visitor, T data)
        {
            return visitor.Visit(this, data);
        }
    }
}
