// -----------------------------------------------------------------------
// <copyright file="Logical.cs" company="">
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
    public class Logical : Expr
    {
        public Expr expr1, expr2;
        public Logical(Token tok, Expr x1, Expr x2)
            : base(tok, null)
        {
            expr1 = x1; expr2 = x2;
            type = check(expr1.type, expr2.type);
            if (type == null)
                error("type error");
        }
        public virtual SType check(SType p1, SType p2)
        {
            if (p1 == SType.Bool && p2 == SType.Bool) return SType.Bool;
            else return null;
        }
        public override Expr gen()
        {
            int f = newlabel();
            int a = newlabel();
            Temp temp = new Temp(type);
            jumping(0, f);
            emit(temp.ToString() + " = true");
            emit("goto L" + a);
            emitlabel(f);
            emit(temp.ToString() + " = false");
            emitlabel(a);
            return temp;
        }
        public override string ToString()
        {
            return expr1.ToString() + " " + op.ToString() + " " + expr2.ToString();
        }
    }
}
