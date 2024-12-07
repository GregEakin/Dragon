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

public class Logical : Expr
{
    public Expr Expr1 { get; }
    public Expr Expr2 { get; }

    public Logical(Token tok, Expr x1, Expr x2)
        : base(tok, Check(x1.Type, x2.Type))
    {
        Expr1 = x1;
        Expr2 = x2;
    }

    protected Logical(Token tok, Expr x1, Expr x2, VarType? type)
        : base(tok, type)
    {
        Expr1 = x1;
        Expr2 = x2;
    }

    private static VarType? Check(VarType? p1, VarType? p2)
    {
        return p1 == VarType.BOOL && p2 == VarType.BOOL 
            ? VarType.BOOL 
            : null;
    }

    public override Expr Gen()
    {
        var f = NewLabel();
        var a = NewLabel();
        var temp = new Temp(Type);
        Jumping(0, f);
        Emit($"{temp} = true");
        Emit($"goto L{a}");
        EmitLabel(f);
        Emit($"{temp} = false");
        EmitLabel(a);
        return temp;
    }

    public override string ToString()
    {
        return $"{Expr1} {Op} {Expr2}";
    }
}