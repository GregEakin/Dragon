// -----------------------------------------------------------------------
// <copyright file="Op.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using Lexical;
using Symbols;

namespace Inter
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Op : Expr
    {
        public Op(Token tok, VarType p)
            : base(tok, p)
        { }

        public override Expr Reduce()
        {
            Expr x = Gen();
            Temp t = new Temp(type);
            Emit(t + " = " + x);
            return t;
        }
    }
}
