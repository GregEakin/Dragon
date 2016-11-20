using System.IO;
using ConsoleX;
using Inter;
using Lexical;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Symbols;

namespace ConsoleTests.Inter
{
    [TestClass]
    public class LogicalTests
    {
        [TestMethod]
        public void LogicalCtor1Test()
        {
            var token = new Token('&');
            var exp1 = new Expr(Word.TRUE, VarType.BOOL);
            var exp2 = new Expr(Word.FALSE, VarType.BOOL);
            var and = new And(token, exp1, exp2);

            Assert.AreSame(exp1, and.Expr1);
            Assert.AreSame(exp2, and.Expr2);
            Assert.AreSame(token, and.Op);
            Assert.AreSame(VarType.BOOL, and.Type);
        }

        [TestMethod]
        [ExpectedException(typeof(Error))]
        public void LogicalCtor2Test()
        {
            var token = new Token('&');
            var exp1 = new Expr(new Word("x", Tag.ID), VarType.INT);
            var exp2 = new Expr(new Word("y", Tag.ID), VarType.INT);
            var and = new And(token, exp1, exp2);
        }

        [TestMethod]
        public void LogicalGenTest()
        {
            using (var cout = new StringWriter())
            {
                Node.Cout = cout;

                var token = new Token('&');
                var exp1 = new Expr(Word.TRUE, VarType.BOOL);
                var exp2 = new Expr(Word.FALSE, VarType.BOOL);
                var and = new And(token, exp1, exp2);

                var exp = and.Gen();
                Assert.IsInstanceOfType(exp, typeof(Temp));
                Assert.AreEqual(VarType.BOOL, exp.Type);

                var actual = cout.ToString();
                Assert.AreEqual("\tif true goto L1\r\n\tiffalse false goto L1\r\n\tt1 = true\r\n\tgoto L2\r\nL1:\r\n\tt1 = false\r\nL2:\r\n", actual);
            }
        }

        [TestMethod]
        public void LogicalToStringTest()
        {
            var token = new Token('&');
            var exp1 = new Expr(Word.TRUE, VarType.BOOL);
            var exp2 = new Expr(Word.FALSE, VarType.BOOL);
            var and = new And(token, exp1, exp2);

            Assert.AreEqual("true & false", and.ToString());
        }
    }
}