// -----------------------------------------------------------------------
// <copyright file="Lexer.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Lexical
{
    using System;
    using System.IO;
    using System.Collections.Generic;
    using System.Text;
    using Symbols;

    public class Lexer
    {
        private readonly Dictionary<string, Word> _words = new Dictionary<string, Word>();

        private static TextReader Cin { get; set; } = Console.In;

        private char _peek = ' ';

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

        public static int Line { get; private set; } = 1;

        private void ReadCh()
        {
            _peek = (char)Cin.Read();
        }

        private bool ReadCh(char c)
        {
            ReadCh();
            if (_peek != c)
                return false;
            _peek = ' ';
            return true;
        }

        private bool ReadChAgain(char c)
        {
            if (_peek != c)
                return false;
            _peek = ' ';
            return true;
        }

        public Token Scan()
        {
            for (; ; ReadCh())
            {
                if (_peek == ' ' || _peek == '\t' || _peek == '\r')
                    continue;
                if (_peek == '\n')
                    Line = Line + 1;
                else
                    break;
            }

            switch (_peek)
            {
                case '=':
                    return ReadCh('=') ? Word.EQ : new Token('=');
                case '<':
                    return ReadCh('>') ? Word.NE : (ReadChAgain('=') ? Word.LE : new Token('<'));
                case '>':
                    return ReadCh('=') ? Word.GE : new Token('>');
            }

            if (char.IsDigit(_peek))
            {
                var v = 0;
                do
                {
                    v = 10 * v + (int)char.GetNumericValue(_peek);
                    ReadCh();
                }
                while (char.IsDigit(_peek));
                if (_peek != '.')
                    return new Num(v);
                var x = (float)v;
                var d = 10.0f;
                while (true)
                {
                    ReadCh();
                    if (!char.IsDigit(_peek))
                        break;
                    x = x + (int)char.GetNumericValue(_peek) / d;
                    d = d * 10.0f;
                }
                return new Real(x);
            }

            if (char.IsLetter(_peek))
            {
                var b = new StringBuilder();
                do
                {
                    b.Append(_peek);
                    ReadCh();
                }
                while (char.IsLetterOrDigit(_peek));
                var s = b.ToString();
                if (_words.ContainsKey(s))
                    return _words[s];
                var w = new Word(s, Tag.ID);
                _words.Add(s, w);
                return w;
            }

            var tok = new Token(_peek);
            _peek = ' ';
            return tok;
        }

        private void Reserve(Word w)
        {
            _words.Add(w.Lexeme, w);
        }
    }
}
