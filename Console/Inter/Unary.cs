// -----------------------------------------------------------------------
// <copyright file="Unary.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Inter
{
    using Lexical;
    using Symbols;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Unary : Op
    {
        public readonly Expr expr;

        public Unary(Token tok, Expr x)
            : base(tok, null)
        {
            expr = x;
            type = VarType.max(VarType.INT, expr.type);
            if (type == null)
                Error("type error");
        }

        public override Expr Gen()
        {
            return new Unary(op, expr.Reduce());
        }

        public override string ToString()
        {
            return op.ToString() + " " + expr.ToString();
        }
    }
}
