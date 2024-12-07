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

public class AndTests
{
    [Fact]
    public void AndCtorTest()
    {
        var token = new Word("and", Tag.AND);
        var exp1 = new Expr(Word.TRUE, VarType.BOOL);
        var exp2 = new Expr(Word.FALSE, VarType.BOOL);
        var and = new And(token, exp1, exp2);

        Assert.Same(exp1, and.Expr1);
        Assert.Same(exp2, and.Expr2);
        Assert.Same(token, and.Op);
        Assert.Equal(VarType.BOOL, and.Type);
    }

    [Fact]
    public void AndJumpingTest()
    {
        using var cout = new StringWriter();
        Node.Cout = cout;

        var token = new Word("and", Tag.AND);
        var exp1 = new Expr(Word.TRUE, VarType.BOOL);
        var exp2 = new Expr(Word.FALSE, VarType.BOOL);
        var and = new And(token, exp1, exp2);
        and.Jumping(22, 33);

        var actual = cout.ToString();
        Assert.Equal("\tif true goto L33\r\n\tif false goto L22\r\n\tgoto L33\r\n", actual);
    }
}