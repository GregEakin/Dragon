﻿// Copyright 2024 Gregory Eakin
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

namespace Dragon.Inter;

public class Break : Stmt
{
    private readonly Stmt _stmt;

    public Break()
    {
        _stmt = Enclosing ?? throw new Error($"near line {Lexline}: unenclosed  break");
    }

    public override void Gen(int b, int a)
    {
        Emit($"goto L{_stmt.After}");
    }
}