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
            : base(tok, Check(x1.type, x2.type))
        {
            expr1 = x1;
            expr2 = x2;
        }

        protected Logical(Token tok, Expr x1, Expr x2, VarType type)
            : base(tok, type)
        {
            expr1 = x1;
            expr2 = x2;
        }

        private static VarType Check(VarType p1, VarType p2)
        {
            if (p1 == VarType.BOOL && p2 == VarType.BOOL)
                return VarType.BOOL;
            else
                return null;
        }

        public override Expr Gen()
        {
            var f = NewLabel();
            var a = NewLabel();
            var temp = new Temp(type);
            Jumping(0, f);
            Emit(temp + " = true");
            Emit("goto L" + a);
            EmitLabel(f);
            Emit(temp + " = false");
            EmitLabel(a);
            return temp;
        }

        public override string ToString()
        {
            return expr1 + " " + op + " " + expr2;
        }
    }
}
