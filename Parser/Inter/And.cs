// -----------------------------------------------------------------------
// <copyright file="And.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using Lexical;

namespace Inter
{
    public class And : Logical
    {
        public And(Token tok, Expr x1, Expr x2)
            : base(tok, x1, x2)
        { }

        public override void Jumping(int t, int f)
        {
            var label = f != 0 ? f : NewLabel();
            Expr1.Jumping(label, 0);
            Expr2.Jumping(t, f);
            if (f == 0)
                EmitLabel(label);
        }
    }
}
