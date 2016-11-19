// -----------------------------------------------------------------------
// <copyright file="Unary.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using Lexical;
using Symbols;

namespace Inter
{
    public class Unary : Op
    {
        private readonly Expr _expr;

        public Unary(Token tok, Expr x)
            : base(tok, VarType.Max(VarType.INT, x.Type))
        {
            _expr = x;
        }

        public override Expr Gen()
        {
            return new Unary(Op, _expr.Reduce());
        }

        public override string ToString()
        {
            return Op + " " + _expr;
        }
    }
}
