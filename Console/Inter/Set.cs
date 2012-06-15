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
        public readonly Id id;
        public readonly Expr expr;

        public Set(Id i, Expr x)
        {
            id = i;
            expr = x;
            if (Check(id.type, expr.type) == null)
                Error("type error");
        }

        public SType Check(SType p1, SType p2)
        {
            if (SType.numeric(p1) && SType.numeric(p2))
                return p2;
            else if (p1 == SType.Bool && p2 == SType.Bool)
                return p2;
            else
                return null;
        }

        public override void Gen(int b, int a)
        {
            Emit(id.ToString() + " = " + expr.Gen().ToString());
        }
    }
}
