// -----------------------------------------------------------------------
// <copyright file="Set.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Inter
{
    using Symbols;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Set : Stmt
    {
        public Id id;
        public Expr expr;
        public Set(Id i, Expr x)
        {
            id = i;
            expr = x;
            if (check(id.type, expr.type) == null)
                error("type error");
        }
        public SType check(SType p1, SType p2)
        {
            if (SType.numeric(p1) && SType.numeric(p2))
                return p2;
            else if (p1 == SType.Bool && p2 == SType.Bool)
                return p2;
            else
                return null;
        }
        public override void gen(int b, int a)
        {
            emit(id.ToString() + " = " + expr.gen().ToString());
        }
    }
}
