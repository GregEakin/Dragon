// -----------------------------------------------------------------------
// <copyright file="Arith.cs" company="">
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
    public class Arith : Op
    {
        public readonly Expr expr1;
        public readonly Expr expr2;

        public Arith(Token tok, Expr x1, Expr x2)
            : base(tok, VarType.max(x1.type, x2.type))
        {
            expr1 = x1;
            expr2 = x2;
        }

        public override Expr Gen()
        {
            return new Arith(op, expr1.Reduce(), expr2.Reduce());
        }

        public override string ToString()
        {
            return expr1 + " " + op + " " + expr2;
        }
    }
}
