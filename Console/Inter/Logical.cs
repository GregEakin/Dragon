﻿// -----------------------------------------------------------------------
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

        public virtual VarType Check(VarType p1, VarType p2)
        {
            if (p1 == VarType.BOOL && p2 == VarType.BOOL)
                return VarType.BOOL;
            else
                return null;
        }

        public override Expr Gen()
        {
            int f = NewLabel();
            int a = NewLabel();
            Temp temp = new Temp(type);
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
