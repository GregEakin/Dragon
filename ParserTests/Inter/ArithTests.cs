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

using Dragon.Inter;
using Dragon.Lexical;
using Dragon.Symbols;

namespace DragonTests.Inter;

public class ArithTests
{
    [Fact]
    public void ArithCtorTest()
    {
        var token = new Token('+');
        var variable = new Id(new Word("x", Tag.ID), VarType.INT, 0);
        var constant = new Constant(new Num(12), VarType.INT);
        var arith = new Arith(token, variable, constant);

        Assert.Equal(variable, arith.Expr1);
        Assert.Equal(constant, arith.Expr2);
        Assert.Equal(token, arith.Op);
        Assert.Equal(VarType.INT, arith.Type);
        Assert.Equal("x + 12", arith.ToString());
    }

    [Fact]
    public void ArithGenTest()
    {
        var token = new Token('+');
        var variable = new Id(new Word("x", Tag.ID), VarType.INT, 0);
        var constant = new Constant(new Num(12), VarType.INT);
        var arith = new Arith(token, variable, constant);

        var expression = arith.Gen();
        Assert.Equal(VarType.INT, expression.Type);
        Assert.Equal(token, expression.Op);
        Assert.Equal("x + 12", expression.ToString());
    }

    [Fact]
    public void ArithJumpingTest()
    {
        using var cout = new StringWriter();
        Node.Cout = cout;

        var token = new Token('+');
        var variable = new Id(new Word("x", Tag.ID), VarType.INT, 0);
        var constant = new Constant(new Num(12), VarType.INT);
        var arith = new Arith(token, variable, constant);
        arith.Jumping(22, 33);

        var actual = cout.ToString();
        Assert.Equal("\tif x + 12 goto L22\r\n\tgoto L33\r\n", actual);
    }
}