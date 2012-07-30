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

        private static int line = 1;

        private char peek = ' ';

        public Lexer()
        {
            Reserve(new Word("if", Tag.IF));
            Reserve(new Word("else", Tag.ELSE));
            Reserve(new Word("while", Tag.WHILE));
            Reserve(new Word("do", Tag.DO));
            Reserve(new Word("break", Tag.BREAK));
            Reserve(new Word("and", Tag.AND));
            Reserve(new Word("or", Tag.OR));
            Reserve(new Word("not", Tag.NOT));
            Reserve(Word.TRUE);
            Reserve(Word.FALSE);
            Reserve(VarType.INT);
            Reserve(VarType.CHAR);
            Reserve(VarType.BOOL);
            Reserve(VarType.FLOAT);
        }

        public static int Line
        {
            get
            {
                return line;
            }
        }

        private void ReadCh()
        {
            peek = (char)Console.Read();
        }

        private bool ReadCh(char c)
        {
            ReadCh();
            if (peek != c)
                return false;
            peek = ' ';
            return true;
        }

        private bool ReadChAgain(char c)
        {
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
                    line = line + 1;
                else
                    break;
            }

            switch (peek)
            {
                case '=':
                    if (ReadCh('=')) return Word.EQ;
                    else return new Token('=');
                case '<':
                    if (ReadCh('>')) return Word.NE;
                    if (ReadChAgain('=')) return Word.LE;
                    else return new Token('<');
                case '>':
                    if (ReadCh('=')) return Word.GE;
                    else return new Token('>');
            }

            if (char.IsDigit(peek))
            {
                int v = 0;
                do
                {
                    v = 10 * v + (int)char.GetNumericValue(peek);
                    ReadCh();
                }
                while (char.IsDigit(peek));
                if (peek != '.')
                    return new Num(v);
                float x = v;
                float d = 10.0f;
                while (true)
                {
                    ReadCh();
                    if (!char.IsDigit(peek))
                        break;
                    x = x + (int)char.GetNumericValue(peek) / d;
                    d = d * 10.0f;
                }
                return new Real(x);
            }

            if (char.IsLetter(peek))
            {
                var b = new StringBuilder();
                do
                {
                    b.Append(peek);
                    ReadCh();
                }
                while (char.IsLetterOrDigit(peek));
                var s = b.ToString();
                if (words.ContainsKey(s))
                    return words[s];
                var w = new Word(s, Tag.ID);
                words.Add(s, w);
                return w;
            }

            var tok = new Token(peek);
            peek = ' ';
            return tok;
        }

        private void Reserve(Word w)
        {
            words.Add(w.Lexeme, w);
        }
    }
}
