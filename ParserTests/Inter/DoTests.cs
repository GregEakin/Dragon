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

public class DoTests
{
    [Fact]
    public void DoNonBoolCtorTest()
    {
        var variable = new Id(new Word("x", Tag.ID), VarType.INT, 0);
        var constant = new Constant(new Num(12), VarType.INT);
        var expression = new Arith(Word.PLUS, variable, constant);

        var doNode = new Do();
        Assert.Throws<Error>(() => doNode.Init(expression, new Stmt()));
    }

    [Fact]
    public void DoCtorTest()
    {
        using var cout = new StringWriter();
        Node.Cout = cout;

        var variable = new Id(new Word("x", Tag.ID), VarType.INT, 0);
        var constant = new Constant(new Num(12), VarType.INT);
        var expression = new Rel(Word.EQ, variable, constant);

        var doNode = new Do();
        doNode.Init(expression, new Stmt());
        doNode.Gen(11, 22);

        var actual = cout.ToString();
        Assert.Equal("L1:\r\n\tif x == 12 goto L11\r\n", actual);
    }

    [Fact]
    public void DoGenTest()
    {
        using var cout = new StringWriter();
        Node.Cout = cout;

        var variable = new Id(new Word("x", Tag.ID), VarType.INT, 0);
        var constant = new Constant(new Num(12), VarType.INT);
        var expression = new Rel(Word.EQ, variable, constant);

        var doNode = new Do();
        doNode.Init(expression, new Stmt());
        doNode.Gen(11, 22);

        var actual = cout.ToString();
        Assert.Equal("L1:\r\n\tif x == 12 goto L11\r\n", actual);
    }
}