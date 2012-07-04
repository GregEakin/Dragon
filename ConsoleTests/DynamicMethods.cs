namespace Dragon
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection.Emit;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class DynamicMethods
    {
        static int CSharpFact(int value)
        {
            int result = 1;
            while (value > 1)
            {
                result *= value--;
            }
            return result;
        }

        static Func<int, int> ILFact()
        {
            var method = new DynamicMethod("factorial", typeof(int), new[] { typeof(int) });
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

        static Func<int, int> ETFact()
        {
            ParameterExpression value = Expression.Parameter(typeof(int), "value");
            ParameterExpression result = Expression.Parameter(typeof(int), "result");
            LabelTarget label = Expression.Label(typeof(int));
            BlockExpression block = Expression.Block(
                new[] { result },
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

        [TestMethod]
        public void CheckResults()
        {
            Assert.AreEqual(120, CSharpFact(5));
            Assert.AreEqual(120, ILFact()(5));
            Assert.AreEqual(120, ETFact()(5));
        }
    }
}
