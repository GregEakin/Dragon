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

namespace Dragon.Inter;

public class Else : Stmt
{
    private readonly Expr _expr;
    private readonly Stmt _stmt1;
    private readonly Stmt _stmt2;

    public Else(Expr x, Stmt s1, Stmt s2)
    {
        _expr = x;
        _stmt1 = s1;
        _stmt2 = s2;
        if (_expr.Type != VarType.BOOL)
            throw new Error($"near line {_expr.Lexline}: boolean required in if, not {_expr.Type}");
    }

    public override void Gen(int b, int a)
    {
        var label1 = NewLabel();
        var label2 = NewLabel();
        _expr.Jumping(0, label2);
        EmitLabel(label1);
        _stmt1.Gen(label1, a);
        Emit($"goto L{a}");
        EmitLabel(label2);
        _stmt2.Gen(label2, a);
    }
}