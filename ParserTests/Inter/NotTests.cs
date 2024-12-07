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

using Dragon.Inter;
using Dragon.Lexical;
using Dragon.Symbols;

namespace DragonTests.Inter;

public class NotTests
{
    [Fact]
    public void NotCtorTest()
    {
        var token = new Token('!');
        var exp = new Expr(new Word("x", Tag.ID), VarType.BOOL);
        var not = new Not(token, exp);

        Assert.Same(token, not.Op);
        Assert.Same(exp, not.Expr1);
        Assert.Same(exp, not.Expr2);
    }

    [Fact]
    public void NotJumpingTest()
    {
        using var cout = new StringWriter();
        Node.Cout = cout;

        var token = new Token('!');
        var exp = new Expr(new Word("x", Tag.ID), VarType.BOOL);
        var not = new Not(token, exp);
        not.Jumping(22, 33);

        var actual = cout.ToString();
        Assert.Equal("\tif x goto L33\r\n\tgoto L22\r\n", actual);
    }

    [Fact]
    public void NotToStringTest()
    {
        var token = new Token('!');
        var exp = new Expr(new Word("x", Tag.ID), VarType.BOOL);
        var not = new Not(token, exp);

        Assert.Equal("! x", not.ToString());
    }
}