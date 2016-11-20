using System.IO;
using ConsoleX;
using Inter;
using Lexical;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Symbols;

namespace ConsoleTests.Inter
{
    [TestClass]
    public class RelTests
    {
        [TestMethod]
        [ExpectedException(typeof(Error))]
        public void RelArrayCtorTest()
        {
            var token = new Word("<=", Tag.LE);
            var exp1 = new Id(new Word("x", Tag.ID), new Array(2, VarType.INT), 0);
            var exp2 = new Id(new Word("y", Tag.ID), new Array(2, VarType.INT), 0);
            var rel = new Rel(token, exp1, exp2);
        }

        [TestMethod]
        [ExpectedException(typeof(Error))]
        public void RelNotSameCtorTest()
        {
            var token = new Word("<=", Tag.LE);
            var variable = new Id(new Word("x", Tag.ID), VarType.INT, 0);
            var expression = new Expr(new Token(32), VarType.BOOL);
            var rel = new Rel(token, variable, expression);
        }

        [TestMethod]
        public void RelCtorTest()
        {
            var token = new Word("<=", Tag.LE);
            var variable = new Id(new Word("x", Tag.ID), VarType.INT, 0);
            var intnode = new Constant(3);
            var rel = new Rel(token, variable, intnode);

            Assert.AreSame(variable, rel.Expr1);
            Assert.AreSame(intnode, rel.Expr2);
            Assert.AreSame(token, rel.Op);
            Assert.AreSame(VarType.BOOL, rel.Type);
        }

        [TestMethod]
        public void RelJumpingTest()
        {
            using (var cout = new StringWriter())
            {
                Node.Cout = cout;

                var token = new Word("<=", Tag.LE);
                var variable = new Id(new Word("x", Tag.ID), VarType.INT, 0);
                var intnode = new Constant(3);
                var rel = new Rel(token, variable, intnode);

                rel.Jumping(11, 22);

                var actual = cout.ToString();
                Assert.AreEqual("\tif x <= 3 goto L11\r\n\tgoto L22\r\n", actual);
            }
        }
    }
}