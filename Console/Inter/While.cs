// -----------------------------------------------------------------------
// <copyright file="While.cs" company="">
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
    public class While : Stmt
    {
        private Expr expr;
        private Stmt stmt;

        public void Init(Expr x, Stmt s)
        {
            expr = x;
            stmt = s;
            if (expr.type != VarType.BOOL)
                throw new Error("near line " + expr.lexline + ": boolean required in while, not " + expr.type);
        }

        public override void Gen(int b, int a)
        {
            After = a;
            expr.Jumping(0, a);
            int label = NewLabel();
            EmitLabel(label);
            stmt.Gen(label, b);
            Emit("goto L" + b);
        }
    }
}
