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

public class OpTests
{
    [Fact]
    public void OpCtorTest()
    {
        var token = new Word("x", Tag.ID);
        var op = new Op(token, VarType.INT);

        Assert.Same(token, op.Op);
        Assert.Same(VarType.INT, op.Type);
    }

    [Fact]
    public void NotReduceTest()
    {
        using var cout = new StringWriter();
        Node.Cout = cout;

        var token = new Word("x", Tag.ID);
        var op = new Op(token, VarType.INT);
        var exp = op.Reduce();
        Assert.IsType<Temp>(exp);
        Assert.Equal(VarType.INT, exp.Type);

        var actual = cout.ToString();
        Assert.Equal("\tt1 = x\r\n", actual);
    }
}