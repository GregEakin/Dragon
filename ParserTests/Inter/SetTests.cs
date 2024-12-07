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

public class SetTests
{
    [Fact]
    public void SetCtorNumericTest()
    {
        var variable = new Id(new Word("x", Tag.ID), VarType.INT, 0);
        var constant = new Constant(new Num(12), VarType.INT);
        var set = new Set(variable, constant);

        Assert.Equal(variable, set.Id);
        Assert.Equal(constant, set.Expr);
    }

    [Fact]
    public void SetCtorBoolTest()
    {
        var variable = new Id(new Word("x", Tag.ID), VarType.BOOL, 0);
        var constant = new Constant(Word.FALSE, VarType.BOOL);
        var set = new Set(variable, constant);

        Assert.Equal(variable, set.Id);
        Assert.Equal(constant, set.Expr);
    }

    [Fact]
    public void SetCtorInvalidTest()
    {
        var variable = new Id(new Word("x", Tag.ID), VarType.INT, 0);
        var constant = new Constant(Word.FALSE, VarType.BOOL);
        Assert.Throws<Error>(() => _ = new Set(variable, constant));
    }

    [Fact]
    public void SetGenTest()
    {
        using var cout = new StringWriter();
        Node.Cout = cout;

        var variable = new Id(new Word("x", Tag.ID), VarType.INT, 0);
        var constant = new Constant(new Num(12), VarType.INT);
        var set = new Set(variable, constant);
        set.Gen(1, 2);

        Assert.Equal("\tx = 12\r\n", cout.ToString());
    }
}