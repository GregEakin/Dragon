// -----------------------------------------------------------------------
// <copyright file="Logical.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using Lexical;
using Symbols;

namespace Inter
{
    public class Logical : Expr
    {
        public Expr Expr1 { get; }
        public Expr Expr2 { get; }

        public Logical(Token tok, Expr x1, Expr x2)
            : base(tok, Check(x1.Type, x2.Type))
        {
            Expr1 = x1;
            Expr2 = x2;
        }

        protected Logical(Token tok, Expr x1, Expr x2, VarType type)
            : base(tok, type)
        {
            Expr1 = x1;
            Expr2 = x2;
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
            var temp = new Temp(Type);
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
            return Expr1 + " " + Op + " " + Expr2;
        }
    }
}
