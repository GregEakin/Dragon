using System.IO;
using Inter;
using Lexical;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Symbols;

namespace ConsoleTests.Inter
{
    [TestClass]
    public class NotTests
    {
        [TestMethod]
        public void NotCtorTest()
        {
            var token = new Token('!');
            var exp = new Expr(new Word("x", Tag.ID), VarType.BOOL);
            var not = new Not(token, exp);

            Assert.AreSame(token, not.Op);
            Assert.AreSame(exp, not.Expr1);
            Assert.AreSame(exp, not.Expr2);
        }

        [TestMethod]
        public void NotJumpingTest()
        {
            using (var cout = new StringWriter())
            {
                Node.Cout = cout;

                var token = new Token('!');
                var exp = new Expr(new Word("x", Tag.ID), VarType.BOOL);
                var not = new Not(token, exp);
                not.Jumping(22, 33);

                var actual = cout.ToString();
                Assert.AreEqual("\tif x goto L33\r\n\tgoto L22\r\n", actual);
            }
        }

        [TestMethod]
        public void NotToStringTest()
        {
            var token = new Token('!');
            var exp = new Expr(new Word("x", Tag.ID), VarType.BOOL);
            var not = new Not(token, exp);

            Assert.AreEqual("! x", not.ToString());
        }
    }
}