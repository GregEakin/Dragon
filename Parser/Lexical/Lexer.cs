// Copyright 2024 Gregory Eakin
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//      http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

// This is a C# front-end parser derived from the Dragon book, found in Appendix A.
// Aho, Alfred V., and Alfred V. Aho. Compilers: Principles, Techniques, & Tools. Boston: Pearson / Addison Wesley, 2007. Print.

using System.Text;
using Dragon.Symbols;

namespace Dragon.Lexical;

public class Lexer
{
    private readonly Dictionary<string, Word> _words = new();

    public static TextReader Cin { get; set; } = Console.In;

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
            if (_words.TryGetValue(s, out var scan))
                return scan;
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