using Crosslight.API.Nodes.Implementations.Expressions;
using Crosslight.API.Nodes.Interfaces;
using Crosslight.API.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace Crosslight.API.Nodes.Implementations.Access
{
    public class VariableDeclarationNode : Node, IIdentifierProvider
    {
        public override string Type => nameof(VariableDeclarationNode);

        public string Identifier { get; }
        public SyncedProperty<ExpressionNode, Node> Initializer { get; protected set; }
        // TODO: c# also has bracketed argument list
        // public unsafe fixed byte bs[7];
        public VariableDeclarationNode(string identifier)
        {
            Identifier = identifier;
            Initializer = new SyncedProperty<ExpressionNode, Node>(Children);
        }

        public override string ToString()
        {
            return Type;
        }
    }
}
