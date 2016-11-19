// -----------------------------------------------------------------------
// <copyright file="Word.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Lexical
{
    public class Word : Token
    {
        public string Lexeme { get; }

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
            AND = new Word("and", Lexical.Tag.AND),
            OR = new Word("or", Lexical.Tag.OR),
            NOT = new Word("not", Lexical.Tag.NOT),
            EQ = new Word("==", Lexical.Tag.EQ),
            NE = new Word("<>", Lexical.Tag.NE),
            LE = new Word("<=", Lexical.Tag.LE),
            GE = new Word(">=", Lexical.Tag.GE),
            MINUS = new Word("minus", Lexical.Tag.MINUS),
            PLUS = new Word("plus", Lexical.Tag.PLUS),
            TRUE = new Word("true", Lexical.Tag.TRUE),
            FALSE = new Word("false", Lexical.Tag.FALSE),
            TEMP = new Word("t", Lexical.Tag.TEMP);
    }
}
