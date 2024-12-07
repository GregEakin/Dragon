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

using Dragon;
using Dragon.Inter;
using Dragon.Lexical;
using Dragon.Symbols;
using Array = Dragon.Symbols.Array;

namespace DragonTests.Inter;

public class RelTests
{
    [Fact]
    public void RelArrayCtorTest()
    {
        var token = new Word("<=", Tag.LE);
        var exp1 = new Id(new Word("x", Tag.ID), new Array(2, VarType.INT), 0);
        var exp2 = new Id(new Word("y", Tag.ID), new Array(2, VarType.INT), 0);
        Assert.Throws<Error>(() => _ = new Rel(token, exp1, exp2));
    }

    [Fact]
    public void RelNotSameCtorTest()
    {
        var token = new Word("<=", Tag.LE);
        var variable = new Id(new Word("x", Tag.ID), VarType.INT, 0);
        var expression = new Expr(new Token(32), VarType.BOOL);
        Assert.Throws<Error>(() => _ = new Rel(token, variable, expression));
    }

    [Fact]
    public void RelCtorTest()
    {
        var token = new Word("<=", Tag.LE);
        var variable = new Id(new Word("x", Tag.ID), VarType.INT, 0);
        var intnode = new Constant(3);
        var rel = new Rel(token, variable, intnode);

        Assert.Same(variable, rel.Expr1);
        Assert.Same(intnode, rel.Expr2);
        Assert.Same(token, rel.Op);
        Assert.Same(VarType.BOOL, rel.Type);
    }

    [Fact]
    public void RelJumpingTest()
    {
        using var cout = new StringWriter();
        Node.Cout = cout;

        var token = new Word("<=", Tag.LE);
        var variable = new Id(new Word("x", Tag.ID), VarType.INT, 0);
        var intnode = new Constant(3);
        var rel = new Rel(token, variable, intnode);

        rel.Jumping(11, 22);

        var actual = cout.ToString();
        Assert.Equal("\tif x <= 3 goto L11\r\n\tgoto L22\r\n", actual);
    }
}