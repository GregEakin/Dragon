// -----------------------------------------------------------------------
// <copyright file="Rel.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using Lexical;
using Symbols;

namespace Inter
{
    public class Rel : Logical
    {
        public Rel(Token tok, Expr x1, Expr x2)
            : base(tok, x1, x2, Check(x1.Type, x2.Type))
        { }

        private static VarType Check(VarType p1, VarType p2)
        {
            if (p1 is Array || p2 is Array)
                return null;
            else if (p1 == p2)
                return VarType.BOOL;
            else
                return null;
        }

        public override void Jumping(int t, int f)
        {
            var a = Expr1.Reduce();
            var b = Expr2.Reduce();
            var test = a + " " + Op + " " + b;
            EmitJumps(test, t, f);
        }
    }
}
