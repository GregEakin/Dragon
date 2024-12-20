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

using System.Linq.Expressions;
using System.Reflection.Emit;

namespace ParserTests;

public class DynamicMethods
{
    private static int CSharpFact(int value)
    {
        var result = 1;
        while (value > 1)
        {
            result *= value--;
        }
        return result;
    }

    private static Func<int, int> ILFact()
    {
        var method = new DynamicMethod("factorial", typeof(int), [typeof(int)]);
        var il = method.GetILGenerator();
        var result = il.DeclareLocal(typeof(int));
        var startWhile = il.DefineLabel();
        var returnResult = il.DefineLabel();

        // result = 1
        il.Emit(OpCodes.Ldc_I4_1);
        il.Emit(OpCodes.Stloc, result);

        // if (value <= 1) branch end
        il.MarkLabel(startWhile);
        il.Emit(OpCodes.Ldarg_0);
        il.Emit(OpCodes.Ldc_I4_1);
        il.Emit(OpCodes.Ble_S, returnResult);

        // result *= (value--)
        il.Emit(OpCodes.Ldloc, result);
        il.Emit(OpCodes.Ldarg_0);
        il.Emit(OpCodes.Dup);
        il.Emit(OpCodes.Ldc_I4_1);
        il.Emit(OpCodes.Sub);
        il.Emit(OpCodes.Starg_S, 0);
        il.Emit(OpCodes.Mul);
        il.Emit(OpCodes.Stloc, result);

        // end while
        il.Emit(OpCodes.Br_S, startWhile);

        // return result
        il.MarkLabel(returnResult);
        il.Emit(OpCodes.Ldloc, result);
        il.Emit(OpCodes.Ret);

        return (Func<int, int>)method.CreateDelegate(typeof(Func<int, int>));
    }

    private static Func<int, int> ETFact()
    {
        var value = Expression.Parameter(typeof(int), "value");
        var result = Expression.Parameter(typeof(int), "result");
        var label = Expression.Label(typeof(int));
        var block = Expression.Block(
            [result],
            Expression.Assign(result, Expression.Constant(1)),
            Expression.Loop(
                Expression.IfThenElse(
                    Expression.GreaterThan(value, Expression.Constant(1)),
                    Expression.MultiplyAssign(result,
                        Expression.PostDecrementAssign(value)),
                    Expression.Break(label, result)
                ),
                label
            )
        );

        return Expression.Lambda<Func<int, int>>(block, value).Compile();
    }

    [Fact]
    public void CheckResults()
    {
        Assert.Equal(120, CSharpFact(5));
        Assert.Equal(120, ILFact()(5));
        Assert.Equal(120, ETFact()(5));
    }
}