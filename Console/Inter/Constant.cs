// -----------------------------------------------------------------------
// <copyright file="Constant.cs" company="">
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
    public class Constant : Expr
    {
        public Constant(Token tok, SType p)
            : base(tok, p)
        { }

        public Constant(int i)
            : base(new Num(i), SType.Int)
        { }

        public static readonly Constant
            True = new Constant(Word.True, SType.Bool),
            False = new Constant(Word.False, SType.Bool);

        public override void Jumping(int t, int f)
        {
            if (this == True && t != 0)
                Emit("goto L" + t);
            if (this == False && f != 0)
                Emit("goto L" + f);
        }
    }
}
