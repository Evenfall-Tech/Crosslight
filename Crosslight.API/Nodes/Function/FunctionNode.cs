using Crosslight.API.Util;
using System;
using System.Collections.Generic;

namespace Crosslight.API.Nodes.Function
{
    /// <summary>
    /// FunctionNode represents the function abstraction in the language.
    /// </summary>
    public class FunctionNode : Node
    {
        public override Type Type => typeof(FunctionNode);
        public SyncedList<FunctionParameterNode, Node> Parameters { get; protected set; }
        private readonly SyncedProperty<FunctionReturnTypeNode, Node> returnType;
        private readonly SyncedProperty<FunctionBodyNode, Node> body;
        public FunctionReturnTypeNode ReturnType
        {
            get => returnType.Value;
            set => returnType.Value = value;
        }
        public FunctionBodyNode Body
        {
            get => body.Value;
            set => body.Value = value;
        }
        public string Name { get; }
        public FunctionNode(string name)
        {
            Parameters = new SyncedList<FunctionParameterNode, Node>(Children);
            returnType = new SyncedProperty<FunctionReturnTypeNode, Node>(Children);
            body = new SyncedProperty<FunctionBodyNode, Node>(Children);
            Name = name;
        }
        public override string ToString()
        {
            return "FunctionNode";
        }
        public override object AcceptVisitor(IVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}
