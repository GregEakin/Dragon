// -----------------------------------------------------------------------
// <copyright file="Set.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using ConsoleX;
using Symbols;

namespace Inter
{
    public class Set : Stmt
    {
        public Id Id { get; }
        public Expr Expr { get; }

        public Set(Id i, Expr x)
        {
            Id = i;
            Expr = x;
            if (Check(Id.Type, Expr.Type) == null)
                throw new Error("near line " + Lexline + ": type error for " + Id.Type + ", " + Expr.Type);
        }

        private static VarType Check(VarType p1, VarType p2)
        {
            if (VarType.Numeric(p1) && VarType.Numeric(p2))
                return p2;
            else if (p1 == VarType.BOOL && p2 == VarType.BOOL)
                return p2;
            else
                return null;
        }

        public override void Gen(int b, int a)
        {
            Emit(Id + " = " + Expr.Gen());
        }
    }
}
