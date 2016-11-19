// -----------------------------------------------------------------------
// <copyright file="Op.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using Lexical;
using Symbols;

namespace Inter
{
    public class Op : Expr
    {
        public Op(Token tok, VarType p)
            : base(tok, p)
        { }

        public override Expr Reduce()
        {
            var x = Gen();
            var t = new Temp(Type);
            Emit(t + " = " + x);
            return t;
        }
    }
}
