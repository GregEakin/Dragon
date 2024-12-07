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

namespace DragonTests.Inter;

public class LogicalTests
{
    [Fact]
    public void LogicalCtor1Test()
    {
        var token = new Token('&');
        var exp1 = new Expr(Word.TRUE, VarType.BOOL);
        var exp2 = new Expr(Word.FALSE, VarType.BOOL);
        var and = new And(token, exp1, exp2);

        Assert.Same(exp1, and.Expr1);
        Assert.Same(exp2, and.Expr2);
        Assert.Same(token, and.Op);
        Assert.Same(VarType.BOOL, and.Type);
    }

    [Fact]
    public void LogicalCtor2Test()
    {
        var token = new Token('&');
        var exp1 = new Expr(new Word("x", Tag.ID), VarType.INT);
        var exp2 = new Expr(new Word("y", Tag.ID), VarType.INT);
        Assert.Throws<Error>(() => _ = new And(token, exp1, exp2));
    }

    [Fact]
    public void LogicalGenTest()
    {
        using var cout = new StringWriter();
        Node.Cout = cout;

        var token = new Token('&');
        var exp1 = new Expr(Word.TRUE, VarType.BOOL);
        var exp2 = new Expr(Word.FALSE, VarType.BOOL);
        var and = new And(token, exp1, exp2);

        var exp = and.Gen();
        Assert.IsType<Temp>(exp);
        Assert.Equal(VarType.BOOL, exp.Type);

        var actual = cout.ToString();
        Assert.Equal("\tif true goto L1\r\n\tiffalse false goto L1\r\n\tt1 = true\r\n\tgoto L2\r\nL1:\r\n\tt1 = false\r\nL2:\r\n", actual);
    }

    [Fact]
    public void LogicalToStringTest()
    {
        var token = new Token('&');
        var exp1 = new Expr(Word.TRUE, VarType.BOOL);
        var exp2 = new Expr(Word.FALSE, VarType.BOOL);
        var and = new And(token, exp1, exp2);

        Assert.Equal("true & false", and.ToString());
    }
}