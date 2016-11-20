using System.IO;
using Inter;
using Lexical;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Symbols;

namespace ConsoleTests.Inter
{
    [TestClass]
    public class AndTests
    {
        [TestMethod]
        public void AndCtorTest()
        {
            var token = new Word("and", Tag.AND);
            var exp1 = new Expr(Word.TRUE, VarType.BOOL);
            var exp2 = new Expr(Word.FALSE, VarType.BOOL);
            var and = new And(token, exp1, exp2);

            Assert.AreSame(exp1, and.Expr1);
            Assert.AreSame(exp2, and.Expr2);
            Assert.AreSame(token, and.Op);
            Assert.AreEqual(VarType.BOOL, and.Type);
        }

        [TestMethod]
        public void AndJumpingTest()
        {
            using (var cout = new StringWriter())
            {
                Node.Cout = cout;

                var token = new Word("and", Tag.AND);
                var exp1 = new Expr(Word.TRUE, VarType.BOOL);
                var exp2 = new Expr(Word.FALSE, VarType.BOOL);
                var and = new And(token, exp1, exp2);
                and.Jumping(22, 33);

                var actual = cout.ToString();
                Assert.AreEqual("\tif true goto L33\r\n\tif false goto L22\r\n\tgoto L33\r\n", actual);
            }
        }
    }
}