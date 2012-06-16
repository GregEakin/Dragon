// -----------------------------------------------------------------------
// <copyright file="Word.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Lexical
{

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Word : Token
    {
        public readonly string Lexeme;

        public Word(string s, int tag)
            : base(tag)
        {
            Lexeme = s;
        }

        public override string ToString()
        {
            return Lexeme;
        }

        public static readonly Word
            AND = new Word("&&", Tag.AND),
            OR = new Word("||", Tag.OR),
            EQ = new Word("==", Tag.EQ),
            NE = new Word("<>", Tag.NE),
            LE = new Word("<=", Tag.LE),
            GE = new Word(">=", Tag.GE),
            MINUS = new Word("minus", Tag.MINUS),
            TRUE = new Word("true", Tag.TRUE),
            FALSE = new Word("false", Tag.FALSE),
            TEMP = new Word("t", Tag.TEMP);
    }
}
