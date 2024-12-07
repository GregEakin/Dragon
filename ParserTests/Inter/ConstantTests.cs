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

public class ConstantTests
{
    [Fact]
    public void ConstantTrueTest()
    {
        using var cout = new StringWriter();
        Node.Cout = cout;

        var truenode = Constant.TRUE;
        Assert.Equal(Word.TRUE, truenode.Op);
        Assert.Equal(VarType.BOOL, truenode.Type);
        truenode.Jumping(1, 2);

        Assert.Equal("\tgoto L1\r\n", cout.ToString());
    }

    [Fact]
    public void ConstantFalseTest()
    {
        using var cout = new StringWriter();
        Node.Cout = cout;

        var falsenode = Constant.FALSE;
        Assert.Equal(Word.FALSE, falsenode.Op);
        Assert.Equal(VarType.BOOL, falsenode.Type);
        falsenode.Jumping(1, 2);

        Assert.Equal("\tgoto L2\r\n", cout.ToString());
    }

    [Fact]
    public void ConstantCtorIntTest()
    {
        using var cout = new StringWriter();
        Node.Cout = cout;

        var intnode = new Constant(3);
        // Assert.Equal(Word.FALSE, intnode.Op);
        Assert.Equal(VarType.INT, intnode.Type);
        intnode.Jumping(1, 2);

        // Assert.Equal("\tgoto L2\r\n", cout.ToString());
    }

    [Fact]
    public void ConstantCtorFloatTest()
    {
        using var cout = new StringWriter();
        Node.Cout = cout;

        var floatnode = new Constant(3.0f);
        // Assert.Equal(new Real(3.0f), floatnode.Op);
        Assert.Equal(VarType.FLOAT, floatnode.Type);
        floatnode.Jumping(1, 2);

        //Assert.Equal("\tgoto L2\r\n", cout.ToString());
    }
}