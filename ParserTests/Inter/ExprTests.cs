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

public class ExprTests
{
    [Fact]
    public void ExprNullTypeCtorTest()
    {
        var token = new Word("x", Tag.ID);
        Assert.Throws<Error>(() => _ = new Expr(token, null));
    }

    [Fact]
    public void ExprCtorTest()
    {
        var token = new Word("x", Tag.ID);
        var expr = new Expr(token, VarType.INT);

        Assert.Same(token, expr.Op);
        Assert.Same(VarType.INT, expr.Type);
    }

    [Fact]
    public void ExprGenTest()
    {
        var token = new Word("x", Tag.ID);
        var expr = new Expr(token, VarType.INT);

        Assert.Same(expr, expr.Gen());
    }

    [Fact]
    public void ExprReduceTest()
    {
        var token = new Word("x", Tag.ID);
        var expr = new Expr(token, VarType.INT);

        Assert.Same(expr, expr.Reduce());
    }

    [Fact]
    public void ExprJumpingTest()
    {
        using var cout = new StringWriter();
        Node.Cout = cout;

        var token = new Word("x", Tag.ID);
        var expr = new Expr(token, VarType.INT);
        expr.Jumping(11, 22);

        var actual = cout.ToString();
        Assert.Equal("\tif x goto L11\r\n\tgoto L22\r\n", actual);
    }

    [Fact]
    public void ExprEmitJumpsTest()
    {
        using var cout = new StringWriter();
        Node.Cout = cout;

        var token = new Word("x", Tag.ID);
        var expr = new Expr(token, VarType.INT);
        expr.EmitJumps("test", 11, 22);

        var actual = cout.ToString();
        Assert.Equal("\tif test goto L11\r\n\tgoto L22\r\n", actual);
    }

    [Fact]
    public void ExprToStringTest()
    {
        var token = new Word("x", Tag.ID);
        var expr = new Expr(token, VarType.INT);

        Assert.Equal("x", expr.ToString());
    }
}