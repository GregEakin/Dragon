﻿// Copyright 2024 Gregory Eakin
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

public class TempTests
{
    [Fact]
    public void TempCtorTest()
    {
        var temp = new Temp(VarType.INT);
        Assert.Same(Word.TEMP, temp.Op);
        Assert.Same(VarType.INT, temp.Type);
    }

    [Fact]
    public void TempToStringTest()
    {
        var temp = new Temp(VarType.INT);
        Assert.Equal("t1", temp.ToString());
    }
}