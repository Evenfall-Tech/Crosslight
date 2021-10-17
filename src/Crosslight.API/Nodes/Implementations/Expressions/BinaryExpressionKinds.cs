namespace Crosslight.API.Nodes.Implementations.Expressions
{
    public static class BinaryExpressionKinds
    {
        /// <summary>
        /// a + b
        /// </summary>
        const string AddExpression = "ad";
        /// <summary>
        /// a - b
        /// </summary>
        const string SubtractExpression = "sb";
        /// <summary>
        /// a * b
        /// </summary>
        const string MultiplyExpression = "ml";
        /// <summary>
        /// a / b
        /// </summary>
        const string DivideExpression = "dv";
        /// <summary>
        /// a % b
        /// </summary>
        const string ModuloExpression = "md";
        /// <summary>
        /// a << b
        /// </summary>
        const string LeftShiftExpression = "ls";
        /// <summary>
        /// a >> b
        /// </summary>
        const string RightShiftExpression = "rs";
        /// <summary>
        /// a || b
        /// </summary>
        const string LogicalOrExpression = "lo";
        /// <summary>
        /// a && b
        /// </summary>
        const string LogicalAndExpression = "la";
        /// <summary>
        /// a | 0x1
        /// </summary>
        const string BinaryOrExpression = "bo";
        /// <summary>
        /// a & 0x1
        /// </summary>
        const string BinaryAndExpression = "ba";
        /// <summary>
        /// a ^ 0x1
        /// </summary>
        const string BinaryXorExpression = "xr";
        /// <summary>
        /// a == b
        /// </summary>
        const string EqualsExpression = "eq";
        /// <summary>
        /// a != b
        /// </summary>
        const string NotEqualsExpression = "nq";
        /// <summary>
        /// a < b
        /// </summary>
        const string LessThanExpression = "lt";
        /// <summary>
        /// a <= b
        /// </summary>
        const string LessThanOrEqualExpression = "le";
        /// <summary>
        /// a > b
        /// </summary>
        const string GreaterThanExpression = "gt";
        /// <summary>
        /// a >= b
        /// </summary>
        const string GreaterThanOrEqualExpression = "ge";
        /// <summary>
        ///  Greater if a > b, 0 if equals
        /// </summary>
        const string ThreeWayComparisonExpression = "te";
        /// <summary>
        /// a is MyClass
        /// </summary>
        const string IsExpression = "is";
        /// <summary>
        /// a as MyClass
        /// </summary>
        const string AsExpression = "as";
        /// <summary>
        /// a ?? b
        /// </summary>
        const string CoalesceExpression = "cl";
    }
}
