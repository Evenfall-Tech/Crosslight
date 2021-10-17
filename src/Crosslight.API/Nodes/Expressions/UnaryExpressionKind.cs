using System;
using System.Collections.Generic;
using System.Text;

namespace Crosslight.API.Nodes.Expressions
{
    public static class UnaryExpressionKind
    {
        /// <summary>
        /// -a
        /// </summary>
        const string PrefixPlusExpression = "pl";
        /// <summary>
        /// +a
        /// </summary>
        const string PrefixMinusExpression = "mn";
        /// <summary>
        /// !0x1
        /// </summary>
        const string PrefixBinaryNotExpression = "bn";
        /// <summary>
        /// !a
        /// </summary>
        const string PrefixLogicalNotExpression = "ln";
        /// <summary>
        /// &a
        /// </summary>
        const string PrefixAddrExpression = "ad";
        /// <summary>
        /// *a
        /// </summary>
        const string PrefixPointerExpression = "pr";
        /// <summary>
        /// ++a
        /// </summary>
        const string PrefixIncrementExpression = "ei";
        /// <summary>
        /// --a
        /// </summary>
        const string PrefixDecrementExpression = "ed";
        /// <summary>
        /// a++
        /// </summary>
        const string PostfixIncrementExpression = "oi";
        /// <summary>
        /// a--
        /// </summary>
        const string PostfixDecrementExpression = "od";
        /// <summary>
        /// a!
        /// </summary>
        const string PostfixDenullifyExpression = "dn";
    }
}
