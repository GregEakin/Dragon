// -----------------------------------------------------------------------
// <copyright file="And.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Inter
{
    using Lexical;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class And : Logical
    {
        public And(Token tok, Expr x1, Expr x2)
            : base(tok, x1, x2)
        { }

        public override void Jumping(int t, int f)
        {
            int label = f != 0 ? f : NewLabel();
            expr1.Jumping(label, 0);
            expr2.Jumping(t, f);
            if (f == 0)
                EmitLabel(label);
        }
    }
}
