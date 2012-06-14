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
        Expr expr = null;
        Stmt stmt = null;
        public While()
        {
        }
        public void init(Expr x, Stmt s)
        {
            expr = x;
            stmt = s;
            if (expr.type != SType.Bool)
                expr.error("boolean required in while");
        }
        public override void gen(int b, int a)
        {
            after = a;
            expr.jumping(0, a);
            int label = newlabel();
            emitlabel(label);
            stmt.gen(label, b);
            emit("goto L" + b);
        }
    }
}
