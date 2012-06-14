// -----------------------------------------------------------------------
// <copyright file="Rel.cs" company="">
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
    public class Rel : Logical
    {
        public Rel(Token tok, Expr x1, Expr x2) : base(tok, x1, x2) { }
        public override SType check(Symbols.SType p1, Symbols.SType p2)
        {
            if (p1 is Array || p2 is Array)
                return null;
            else if (p1 == p2) return SType.Bool;
            else return null;
        }
        public override void jumping(int t, int f)
        {
            Expr a = expr1.reduce();
            Expr b = expr2.reduce();
            string test = a.ToString() + " " + op.ToString() + " " + b.ToString();
            emitjumps(test, t, f);
        }
    }
}
