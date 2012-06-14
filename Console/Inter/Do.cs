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
        Expr expr = null;
        Stmt stmt = null;
        public Do()
        {
        }
        public void init(Stmt s, Expr x)
        {
            expr = x;
            stmt = s;
            if (expr.type != SType.Bool)
                expr.error("boolean required in while");
        }
        public override void gen(int b, int a)
        {
            after = a;
            int label = newlabel();
            stmt.gen(b, label);
            emitlabel(label);
            expr.jumping(b, 0);
        }
    }
}
