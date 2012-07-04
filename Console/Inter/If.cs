// -----------------------------------------------------------------------
// <copyright file="If.cs" company="">
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
    public class If : Stmt
    {
        readonly Expr expr;
        readonly Stmt stmt;

        public If(Expr x, Stmt s)
        {
            expr = x;
            stmt = s;
            if (expr.type != VarType.BOOL)
                throw new Error("near line " + expr.lexline + ": boolean required in if, not " + expr.type);
        }

        public override void Gen(int b, int a)
        {
            int label = NewLabel();
            expr.Jumping(0, a);
            EmitLabel(label);
            stmt.Gen(label, a);
        }
    }
}
