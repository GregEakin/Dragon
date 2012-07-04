// -----------------------------------------------------------------------
// <copyright file="Unary.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using Lexical;
using Symbols;

namespace Inter
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Unary : Op
    {
        public readonly Expr expr;

        public Unary(Token tok, Expr x)
            : base(tok, VarType.max(VarType.INT, x.type))
        {
            expr = x;
        }

        public override Expr Gen()
        {
            return new Unary(op, expr.Reduce());
        }

        public override string ToString()
        {
            return op + " " + expr;
        }
    }
}