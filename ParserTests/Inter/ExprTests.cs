using System.IO;
using ConsoleX;
using Inter;
using Lexical;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Symbols;

namespace ConsoleTests.Inter
{
    [TestClass]
    public class ExprTests
    {
        [TestMethod]
        [ExpectedException(typeof(Error))]
        public void ExprNullTypeCtorTest()
        {
            var toekn = new Word("x", Tag.ID);
            var expr = new Expr(toekn, null);
        }

        [TestMethod]
        public void ExprCtorTest()
        {
            var token = new Word("x", Tag.ID);
            var expr = new Expr(token, VarType.INT);

            Assert.AreSame(token, expr.Op);
            Assert.AreSame(VarType.INT, expr.Type);
        }

        [TestMethod]
        public void ExprGenTest()
        {
            var token = new Word("x", Tag.ID);
            var expr = new Expr(token, VarType.INT);

            Assert.AreSame(expr, expr.Gen());
        }

        [TestMethod]
        public void ExprReduceTest()
        {
            var token = new Word("x", Tag.ID);
            var expr = new Expr(token, VarType.INT);

            Assert.AreSame(expr, expr.Reduce());
        }

        [TestMethod]
        public void ExprJumpingTest()
        {
            using (var cout = new StringWriter())
            {
                Node.Cout = cout;

                var token = new Word("x", Tag.ID);
                var expr = new Expr(token, VarType.INT);
                expr.Jumping(11, 22);

                var actual = cout.ToString();
                Assert.AreEqual("\tif x goto L11\r\n\tgoto L22\r\n", actual);
            }
        }

        [TestMethod]
        public void ExprEmitJumpsTest()
        {
            using (var cout = new StringWriter())
            {
                Node.Cout = cout;

                var token = new Word("x", Tag.ID);
                var expr = new Expr(token, VarType.INT);
                expr.EmitJumps("test", 11, 22);

                var actual = cout.ToString();
                Assert.AreEqual("\tif test goto L11\r\n\tgoto L22\r\n", actual);
            }
        }

        [TestMethod]
        public void ExprToStringTest()
        {
            var token = new Word("x", Tag.ID);
            var expr = new Expr(token, VarType.INT);

            Assert.AreEqual("x", expr.ToString());
        }
    }
}