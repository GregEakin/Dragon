// -----------------------------------------------------------------------
// <copyright file="Lexer.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Lexical
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Symbols;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Lexer
    {
        private readonly Dictionary<string, Word> words = new Dictionary<string, Word>();

        public static int Line = 1;

        private char peek = ' ';

        public Lexer()
        {
            reserve(new Word("if", Tag.IF));
            reserve(new Word("else", Tag.ELSE));
            reserve(new Word("while", Tag.WHILE));
            reserve(new Word("do", Tag.DO));
            reserve(new Word("break", Tag.BREAK));
            reserve(Word.True);
            reserve(Word.False);
            reserve(SType.Int);
            reserve(SType.Char);
            reserve(SType.Bool);
            reserve(SType.Float);
        }

        private void ReadCh()
        {
            peek = (char)Console.Read();
        }

        private Boolean ReadCh(char c)
        {
            ReadCh();
            if (peek != c)
                return false;
            peek = ' ';
            return true;
        }

        public Token Scan()
        {
            for (; ; ReadCh())
            {
                if (peek == ' ' || peek == '\t' || peek == '\r')
                    continue;
                else if (peek == '\n')
                    Line = Line + 1;
                else
                    break;
            }

            switch (peek)
            {
                case '&':
                    if (ReadCh('&')) return Word.and;
                    else return new Token('&');
                case '|':
                    if (ReadCh('|')) return Word.or;
                    else return new Token('|');
                case '=':
                    if (ReadCh('=')) return Word.eq;
                    else return new Token('=');
                case '!':
                    if (ReadCh('=')) return Word.ne;
                    else return new Token('!');
                case '<':
                    if (ReadCh('=')) return Word.le;
                    else return new Token('<');
                case '>':
                    if (ReadCh('=')) return Word.ge;
                    else return new Token('>');
            }

            if (char.IsDigit(peek))
            {
                int v = 0;
                do
                {
                    v = 10 * v + (int)char.GetNumericValue(peek); ReadCh();
                }
                while (char.IsDigit(peek));
                if (peek != '.')
                    return new Num(v);
                float x = v; float d = 10;
                while (true)
                {
                    ReadCh();
                    if (!char.IsDigit(peek))
                        break;
                    x = x + (int)char.GetNumericValue(peek) / d; d = d * 10;
                }
                return new Real(x);
            }

            if (char.IsLetter(peek))
            {
                StringBuilder b = new StringBuilder();
                do
                {
                    b.Append(peek);
                    ReadCh();
                }
                while (char.IsLetterOrDigit(peek));
                string s = b.ToString();
                if (words.ContainsKey(s))
                    return words[s];
                Word w = new Word(s, Tag.ID);
                words.Add(s, w);
                return w;
            }

            Token tok = new Token(peek);
            peek = ' ';
            return tok;
        }

        private void reserve(Word w)
        {
            words.Add(w.Lexeme, w);
        }
    }
}
