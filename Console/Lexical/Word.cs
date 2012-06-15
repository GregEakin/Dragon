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
            and = new Word("&&", Tag.AND),
            or = new Word("||", Tag.OR),
            eq = new Word("==", Tag.EQ),
            ne = new Word("!=", Tag.NE),
            le = new Word("<=", Tag.LE),
            ge = new Word(">=", Tag.GE),
            minus = new Word("minus", Tag.MINUS),
            True = new Word("true", Tag.TRUE),
            False = new Word("false", Tag.FALSE),
            temp = new Word("t", Tag.TEMP);
    }
}
