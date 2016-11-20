using System.IO;
using Inter;
using Lexical;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Symbols;

namespace ConsoleTests.Inter
{
    [TestClass]
    public class OrTests
    {
        [TestMethod]
        public void OrCtorTest()
        {
            var token = new Word("or", Tag.OR);
            var exp1 = new Expr(Word.TRUE, VarType.BOOL);
            var exp2 = new Expr(Word.FALSE, VarType.BOOL);
            var or = new Or(token, exp1, exp2);

            Assert.AreSame(exp1, or.Expr1);
            Assert.AreSame(exp2, or.Expr2);
            Assert.AreSame(token, or.Op);
            Assert.AreEqual(VarType.BOOL, or.Type);
        }

        [TestMethod]
        public void OrJumpingTest()
        {
            using (var cout = new StringWriter())
            {
                Node.Cout = cout;

                var token = new Word("or", Tag.OR);
                var exp1 = new Expr(Word.TRUE, VarType.BOOL);
                var exp2 = new Expr(Word.FALSE, VarType.BOOL);
                var or = new Or(token, exp1, exp2);
                or.Jumping(22, 33);

                var actual = cout.ToString();
                Assert.AreEqual("\tif true goto L22\r\n\tif false goto L22\r\n\tgoto L33\r\n", actual);
            }
        }
    }
}