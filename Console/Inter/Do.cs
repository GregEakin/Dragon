// -----------------------------------------------------------------------
// <copyright file="Do.cs" company="">
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
    public class Do : Stmt
    {
        Expr expr;
        Stmt stmt;

        public Do()
        {
        }

        public void Init(Expr x, Stmt s)
        {
            expr = x;
            stmt = s;
            if (expr.type != VarType.BOOL)
                throw new Error("near line " + expr.lexline + ": boolean required in while, not " + expr.type);
        }

        public override void Gen(int b, int a)
        {
            after = a;
            int label = NewLabel();
            stmt.Gen(b, label);
            EmitLabel(label);
            expr.Jumping(b, 0);
        }
    }
}
