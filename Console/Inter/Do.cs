// -----------------------------------------------------------------------
// <copyright file="Do.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Inter
{
    using Symbols;

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

        public void init(Expr x, Stmt s)
        {
            expr = x;
            stmt = s;
            if (expr.type != SType.Bool)
                expr.Error("boolean required in while");
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
