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

public class Expr : Node
{
    public Token Op { get; }
    public VarType Type { get; }

    public Expr(Token tok, VarType? p)
    {
        Op = tok;
        Type = p ?? throw new Error($"near line {Lexline}: type error");
    }

    public virtual Expr Gen()
    {
        return this;
    }

    public virtual Expr Reduce()
    {
        return this;
    }

    public virtual void Jumping(int t, int f)
    {
        EmitJumps(ToString(), t, f);
    }

    public void EmitJumps(string test, int t, int f)
    {
        if (t != 0 && f != 0)
        {
            Emit($"if {test} goto L{t}");
            Emit($"goto L{f}");
        }
        else if (t != 0) Emit($"if {test} goto L{t}");
        else if (f != 0) Emit($"iffalse {test} goto L{f}");
    }

    public override string ToString()
    {
        return Op.ToString();
    }
}