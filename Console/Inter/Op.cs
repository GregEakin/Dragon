// -----------------------------------------------------------------------
// <copyright file="Op.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Inter
{
    using Lexical;
    using Symbols;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Op : Expr
    {
        public Op(Token tok, SType p) : base(tok, p) { }
        public override Expr reduce()
        {
            Expr x = gen();
            Temp t = new Temp(type);
            emit(t.ToString() + " = " + x.ToString());
            return t;
        }
    }
}
