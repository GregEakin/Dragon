// -----------------------------------------------------------------------
// <copyright file="Else.cs" company="">
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
    public class Else : Stmt
    {
        readonly Expr expr;
        readonly Stmt stmt1;
        readonly Stmt stmt2;

        public Else(Expr x, Stmt s1, Stmt s2)
        {
            expr = x;
            stmt1 = s1;
            stmt2 = s2;
            if (expr.type != VarType.BOOL)
                throw new Error("near line " + expr.lexline + ": boolean required in if, not " + expr.type);
        }

        public override void Gen(int b, int a)
        {
            int label1 = NewLabel();
            int label2 = NewLabel();
            expr.Jumping(0, label2);
            EmitLabel(label1);
            stmt1.Gen(label1, a);
            Emit("goto L" + a);
            EmitLabel(label2);
            stmt2.Gen(label2, a);
        }
    }
}
