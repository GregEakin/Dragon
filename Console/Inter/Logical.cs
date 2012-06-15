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
        public readonly Expr expr1;
        public readonly Expr expr2;

        public Logical(Token tok, Expr x1, Expr x2)
            : base(tok, null)
        {
            expr1 = x1;
            expr2 = x2;
            type = Check(expr1.type, expr2.type);
            if (type == null)
                Error("type error");
        }

        public virtual SType Check(SType p1, SType p2)
        {
            if (p1 == SType.Bool && p2 == SType.Bool) return SType.Bool;
            else return null;
        }

        public override Expr Gen()
        {
            int f = NewLabel();
            int a = NewLabel();
            Temp temp = new Temp(type);
            Jumping(0, f);
            Emit(temp.ToString() + " = true");
            Emit("goto L" + a);
            EmitLabel(f);
            Emit(temp.ToString() + " = false");
            EmitLabel(a);
            return temp;
        }

        public override string ToString()
        {
            return expr1.ToString() + " " + op.ToString() + " " + expr2.ToString();
        }
    }
}
