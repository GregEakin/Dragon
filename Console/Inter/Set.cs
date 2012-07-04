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

        private static VarType Check(VarType p1, VarType p2)
        {
            if (VarType.numeric(p1) && VarType.numeric(p2))
                return p2;
            else if (p1 == VarType.BOOL && p2 == VarType.BOOL)
                return p2;
            else
                return null;
        }

        public override void Gen(int b, int a)
        {
            Emit(id + " = " + expr.Gen());
        }
    }
}
