// -----------------------------------------------------------------------
// <copyright file="If.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Inter
{
    using Symbols;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class If : Stmt
    {
        Expr expr;
        Stmt stmt;
        public If(Expr x, Stmt s)
        {
            expr = x;
            stmt = s;
            if (expr.type != SType.Bool)
                expr.error("boolean required in if");
        }
        public override void gen(int b, int a)
        {
            int label = newlabel();
            expr.jumping(0, a);
            emitlabel(label);
            stmt.gen(label, a);
        }
    }
}
