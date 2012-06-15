// -----------------------------------------------------------------------
// <copyright file="While.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Inter
{
    using Symbols;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class While : Stmt
    {
        private Expr expr;
        private Stmt stmt;

        public While()
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
            expr.Jumping(0, a);
            int label = NewLabel();
            EmitLabel(label);
            stmt.Gen(label, b);
            Emit("goto L" + b);
        }
    }
}
