﻿// -----------------------------------------------------------------------
// <copyright file="Constant.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using Lexical;
using Symbols;

namespace Inter
{
    public class Constant : Expr
    {
        public Constant(Token tok, VarType p)
            : base(tok, p)
        { }

        public Constant(int i)
            : base(new Num(i), VarType.INT)
        { }

        public Constant(float d)
            : base(new Real(d), VarType.FLOAT)
        { }

        public static readonly Constant
            TRUE = new Constant(Word.TRUE, VarType.BOOL),
            FALSE = new Constant(Word.FALSE, VarType.BOOL);

        public override void Jumping(int t, int f)
        {
            if (this == TRUE && t != 0)
                Emit("goto L" + t);
            if (this == FALSE && f != 0)
                Emit("goto L" + f);
        }
    }
}
