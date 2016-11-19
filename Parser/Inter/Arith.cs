// -----------------------------------------------------------------------
// <copyright file="Arith.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using Lexical;
using Symbols;

namespace Inter
{
    public class Arith : Op
    {
        public Expr Expr1 { get; }
        public Expr Expr2 { get; }

        public Arith(Token tok, Expr x1, Expr x2)
            : base(tok, VarType.Max(x1.Type, x2.Type))
        {
            Expr1 = x1;
            Expr2 = x2;
        }

        public override Expr Gen()
        {
            return new Arith(Op, Expr1.Reduce(), Expr2.Reduce());
        }

        public override string ToString()
        {
            return Expr1 + " " + Op + " " + Expr2;
        }
    }
}
