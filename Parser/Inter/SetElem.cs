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

using Dragon.Symbols;
using Array = Dragon.Symbols.Array;

namespace Dragon.Inter;

public class SetElem : Stmt
{
    public Id Array { get; }
    public Expr Index { get; }
    public Expr Expr { get; }

    public SetElem(Access x, Expr y)
    {
        Array = x.Array;
        Index = x.Index;
        Expr = y;
        if (Check(x.Type, Expr.Type) == null)
            throw new Error($"near line {Lexline}: type error for {x.Type}, {Expr.Type}");
    }

    private static VarType? Check(VarType? p1, VarType? p2)
    {
        if (p1 is Array || p2 is Array)
            return null;
        else if (p1 == p2)
            return p2;
        else if (VarType.Numeric(p1) && VarType.Numeric(p2))
            return p2;
        else
            return null;
    }

    public override void Gen(int b, int a)
    {
        var s1 = Index.Reduce();
        var s2 = Expr.Reduce();
        Emit($"{Array} [ {s1} ] = {s2}");
    }
}