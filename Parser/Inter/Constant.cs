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
using Dragon.Symbols;

namespace Dragon.Inter;

public class Constant : Expr
{
    public Constant(Token tok, VarType p)
        : base(tok, p)
    { }

    public Constant(int i)
        : base(new Num(i), VarType.INT)
    { }

    public Constant(float d)
        : base(new Real(d), VarType.FLOAT)
    { }

    public static readonly Constant
        TRUE = new(Word.TRUE, VarType.BOOL),
        FALSE = new(Word.FALSE, VarType.BOOL);

    public override void Jumping(int t, int f)
    {
        if (this == TRUE && t != 0)
            Emit($"goto L{t}");
        if (this == FALSE && f != 0)
            Emit($"goto L{f}");
    }
}