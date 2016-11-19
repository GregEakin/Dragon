// -----------------------------------------------------------------------
// <copyright file="SetElem.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using ConsoleX;
using Symbols;

namespace Inter
{
    public class SetElem : Stmt
    {
        public Id Array { get; }
        public Expr Index { get; }
        public Expr Expr { get; }

        public SetElem(Access x, Expr y)
        {
            Array = x.Array;
            Index = x.Index;
            Expr = y;
            if (Check(x.Type, Expr.Type) == null)
                throw new Error("near line " + Lexline + ": type error for " + x.Type + ", " + Expr.Type);
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
            var s1 = Index.Reduce();
            var s2 = Expr.Reduce();
            Emit(Array + " [ " + s1 + " ] = " + s2);
        }
    }
}
