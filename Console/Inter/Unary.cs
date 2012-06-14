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
        public Expr expr;

        public Unary(Token tok, Expr x)
            : base(tok, null)
        {
            type = SType.max(SType.Int, expr.type);
            if (type == null)
                error("type error");
        }

        public override Expr gen()
        {
            return new Unary(op, expr.reduce());
        }

        public override string ToString()
        {
            return op.ToString() + " " + expr.ToString();
        }
    }
}
