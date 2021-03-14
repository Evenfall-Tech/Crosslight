namespace Crosslight.API.Nodes.Access.Modifiers
{
    /// <summary>
    /// <see cref="CustomModifierNode"/> is a stand-in for unusual
    /// modifiers unsupported by the main framework.
    /// </summary>
    public class CustomModifierNode : ModifierNode
    {
        public string CustomToken { get; protected set; }
        public CustomModifierNode(string customToken, ModifierGroup group) : base(ModifierToken.Custom, group)
        {
            CustomToken = customToken;
        }
    }
}
