namespace Crosslight.API.Nodes.Implementations.Expressions
{
    public static class LiteralExpressionKinds
    {
        /// <summary>
        /// Variable argument list (VarArg)
        /// </summary>
        public const string ArgListLiteral = "al";
        /// <summary>
        /// Any integer, fixed- or floating-point decimal
        /// </summary>
        public const string NumericLiteral = "nm";
        /// <summary>
        /// Non-interpolated string
        /// </summary>
        public const string StringLiteral = "st";
        /// <summary>
        /// A single separate character from a string
        /// </summary>
        public const string CharacterLiteral = "cr";
        /// <summary>
        /// True value
        /// </summary>
        public const string BooleanTrueLiteral = "bt";
        /// <summary>
        /// False value
        /// </summary>
        public const string BooleanFalseLiteral = "bf";
        /// <summary>
        /// Null value
        /// </summary>
        public const string NullLiteral = "nl";
        /// <summary>
        /// Default value, which will be deduced during compilation
        /// </summary>
        public const string DefaultLiteral = "df";

        /// <summary>
        /// Long integer or decimal
        /// </summary>
        public const string LongLiteral = "lg";
        /// <summary>
        /// Short integer or decimal
        /// </summary>
        public const string ShortLiteral = "st";
        /// <summary>
        /// Signed integer
        /// </summary>
        public const string SignedLiteral = "sn";
        /// <summary>
        /// Unsigned integer
        /// </summary>
        public const string UnsignedLiteral = "un";
    }
}
