// -----------------------------------------------------------------------
// <copyright file="SetElem.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using ConsoleX;
using Symbols;

namespace Inter
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class SetElem : Stmt
    {
        public readonly Id array;
        public readonly Expr index;
        public readonly Expr expr;

        public SetElem(Access x, Expr y)
        {
            array = x.array;
            index = x.index;
            expr = y;
            if (Check(x.type, expr.type) == null)
                throw new Error("near line " + lexline + ": type error for " + x.type + ", " + expr.type);
        }

        private static VarType Check(VarType p1, VarType p2)
        {
            if (p1 is Array || p2 is Array)
                return null;
            else if (p1 == p2)
                return p2;
            else if (VarType.Numeric(p1) && VarType.Numeric(p2))
                return p2;
            else
                return null;
        }

        public override void Gen(int b, int a)
        {
            var s1 = index.Reduce();
            var s2 = expr.Reduce();
            Emit(array + " [ " + s1 + " ] = " + s2);
        }
    }
}
