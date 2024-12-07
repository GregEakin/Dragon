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

public class UnaryTests
{
    [Fact]
    public void UnaryCtorTest()
    {
        var token = new Token('-');
        var variable = new Id(new Word("x", Tag.ID), VarType.INT, 0);
        var unary = new Unary(token, variable);

        Assert.Same(token, unary.Op);
        Assert.Same(VarType.INT, unary.Type);
    }

    [Fact]
    public void UnaryGenTest()
    {
        var token = new Token('-');
        var variable = new Id(new Word("x", Tag.ID), VarType.INT, 0);
        var unary = new Unary(token, variable);
        var exp = unary.Gen();
        Assert.IsType<Unary>(exp);
        Assert.Equal(VarType.INT, exp.Type);
    }

    [Fact]
    public void UnaryToStringTest()
    {
        var token = new Token('-');
        var variable = new Id(new Word("x", Tag.ID), VarType.INT, 0);
        var unary = new Unary(token, variable);

        Assert.Equal("- x", unary.ToString());
    }
}