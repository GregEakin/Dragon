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

using Dragon.Lexical;

namespace Dragon.Symbols;

public class VarType : Word
{
    public int Width { get; }

    protected VarType(string s, int tag, int w)
        : base(s, tag)
    {
        Width = w;
    }

    public static readonly VarType
        FLOAT = new("float", Lexical.Tag.BASIC, 8),
        INT = new("int", Lexical.Tag.BASIC, 4),
        CHAR = new("char", Lexical.Tag.BASIC, 2),
        BOOL = new("bool", Lexical.Tag.BASIC, 1);

    public static bool Numeric(VarType? p)
    {
        return p == CHAR || p == INT || p == FLOAT;
    }

    public static VarType? Max(VarType? p1, VarType? p2)
    {
        if (!Numeric(p1) || !Numeric(p2))
            return null;
        if (p1 == FLOAT || p2 == FLOAT)
            return FLOAT;
        if (p1 == INT || p2 == INT)
            return INT;
        return CHAR;
    }
}