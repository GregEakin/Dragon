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

public class OrTests
{
    [Fact]
    public void OrCtorTest()
    {
        var token = new Word("or", Tag.OR);
        var exp1 = new Expr(Word.TRUE, VarType.BOOL);
        var exp2 = new Expr(Word.FALSE, VarType.BOOL);
        var or = new Or(token, exp1, exp2);

        Assert.Same(exp1, or.Expr1);
        Assert.Same(exp2, or.Expr2);
        Assert.Same(token, or.Op);
        Assert.Equal(VarType.BOOL, or.Type);
    }

    [Fact]
    public void OrJumpingTest()
    {
        using var cout = new StringWriter();
        Node.Cout = cout;

        var token = new Word("or", Tag.OR);
        var exp1 = new Expr(Word.TRUE, VarType.BOOL);
        var exp2 = new Expr(Word.FALSE, VarType.BOOL);
        var or = new Or(token, exp1, exp2);
        or.Jumping(22, 33);

        var actual = cout.ToString();
        Assert.Equal("\tif true goto L22\r\n\tif false goto L22\r\n\tgoto L33\r\n", actual);
    }
}