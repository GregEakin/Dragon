using System.IO;
using Inter;
using Lexical;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Symbols;

namespace ConsoleTests.Inter
{
    [TestClass]
    public class ArithTests
    {
        [TestMethod]
        public void ArithCtorTest()
        {
            var token = new Token('+');
            var variable = new Id(new Word("x", Tag.ID), VarType.INT, 0);
            var constant = new Constant(new Num(12), VarType.INT);
            var arith = new Arith(token, variable, constant);

            Assert.AreEqual(variable, arith.Expr1);
            Assert.AreEqual(constant, arith.Expr2);
            Assert.AreEqual(token, arith.Op);
            Assert.AreEqual(VarType.INT, arith.Type);
            Assert.AreEqual("x + 12", arith.ToString());
        }

        [TestMethod]
        public void ArithGenTest()
        {
            var token = new Token('+');
            var variable = new Id(new Word("x", Tag.ID), VarType.INT, 0);
            var constant = new Constant(new Num(12), VarType.INT);
            var arith = new Arith(token, variable, constant);

            var expresion = arith.Gen();
            Assert.AreEqual(VarType.INT, expresion.Type);
            Assert.AreEqual(token, expresion.Op);
            Assert.AreEqual("x + 12", expresion.ToString());
        }

        [TestMethod]
        public void ArithJumpingTest()
        {
            using (var cout = new StringWriter())
            {
                Node.Cout = cout;

                var token = new Token('+');
                var variable = new Id(new Word("x", Tag.ID), VarType.INT, 0);
                var constant = new Constant(new Num(12), VarType.INT);
                var arith = new Arith(token, variable, constant);
                arith.Jumping(22, 33);

                var actual = cout.ToString();
                Assert.AreEqual("\tif x + 12 goto L22\r\n\tgoto L33\r\n", actual);
            }
        }
    }
}