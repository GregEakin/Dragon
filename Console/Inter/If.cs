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
        readonly Expr expr;
        readonly Stmt stmt;

        public If(Expr x, Stmt s)
        {
            expr = x;
            stmt = s;
            if (expr.type != SType.Bool)
                expr.Error("boolean required in if");
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
