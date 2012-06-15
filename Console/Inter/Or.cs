// -----------------------------------------------------------------------
// <copyright file="Or.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Inter
{
    using Lexical;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Or : Logical
    {
        public Or(Token tok, Expr x1, Expr x2) 
            : base(tok, x1, x2) 
        { }
        
        public override void Jumping(int t, int f)
        {
            int label = t != 0 ? t : NewLabel();
            expr1.Jumping(label, 0);
            expr2.Jumping(t, f);
            if (t == 0) 
                EmitLabel(label);
        }
    }
}
