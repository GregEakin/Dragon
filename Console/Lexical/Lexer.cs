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
        public static int line = 1;
        char peek = ' ';
        Dictionary<string, Word> words = new Dictionary<string, Word>();
        void reserve(Word w) { words.Add(w.lexeme, w); }
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
        void readch() { peek = (char)Console.Read(); }
        Boolean readch(char c)
        {
            readch();
            if (peek != c)
                return false;
            peek = ' ';
            return true;
        }
        public Token scan()
        {
            while (true)
            {
                readch();
                if (peek == ' ' || peek == '\t')
                    continue;
                else if (peek == '\n')
                    line = line + 1;
                else
                    break;
            }
            switch (peek)
            {
                case '&':
                    if (readch('&')) return Word.and;
                    else return new Token('&');
                case '|':
                    if (readch('|')) return Word.or;
                    else return new Token('|');
                case '=':
                    if (readch('=')) return Word.eq;
                    else return new Token('=');
                case '!':
                    if (readch('=')) return Word.ne;
                    else return new Token('!');
                case '<':
                    if (readch('=')) return Word.le;
                    else return new Token('<');
                case '>':
                    if (readch('&')) return Word.ge;
                    else return new Token('>');
            }
            if (char.IsDigit(peek))
            {
                int v = 0;
                do
                {
                    v = 10 * v + int.Parse(peek); readch();
                } while (char.IsDigit(peek));
                if (peek != '.') return new Num(v);
                float x = v; float d = 10;
                while (true)
                {
                    readch();
                    if (!char.IsDigit(peek))
                        break;
                    x = x + int.Parse(peek) / d; d = d * 10;
                }
                return new Real(x);
            }
            if (char.IsLetter(peek))
            {
                StringBuilder b = new StringBuilder();
                do
                {
                    b.Append(peek);
                    readch();
                } while (char.IsLetterOrDigit(peek));
                string s = b.ToString();
                Word w = (Word)words[s];
                if (w != null) return w;
                w = new Word(s, Tag.ID);
                words.Add(s, w);
                return w;
            }
            Token tok = new Token(peek);
            peek = ' ';
            return tok;
        }
    }
}
