using System.IO;
using ConsoleX;
using Inter;
using Lexical;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Symbols;

namespace ConsoleTests.Inter
{
    [TestClass]
    public class SetTests
    {
        [TestMethod]
        public void SetCtorNumericTest()
        {
            var x = new Word("x", Tag.ID);
            var id = new Id(x, VarType.INT, 0);
            var num = new Num(12);
            var expr = new Constant(num, VarType.INT);
            var set = new Set(id, expr);

            Assert.AreEqual(set.Id, id);
            Assert.AreEqual(set.Expr, expr);
        }

        [TestMethod]
        public void SetCtorBoolTest()
        {
            var x = new Word("x", Tag.ID);
            var id = new Id(x, VarType.BOOL, 0);
            var expr = new Constant(Word.FALSE, VarType.BOOL);
            var set = new Set(id, expr);

            Assert.AreEqual(set.Id, id);
            Assert.AreEqual(set.Expr, expr);
        }

        [TestMethod]
        [ExpectedException(typeof(Error))]
        public void SetCtorInvalidTest()
        {
            var x = new Word("x", Tag.ID);
            var id = new Id(x, VarType.INT, 0);
            var expr = new Constant(Word.FALSE, VarType.BOOL);
            var set = new Set(id, expr);
        }

        [TestMethod]
        public void SetGenTest()
        {
            using (var cout = new StringWriter())
            {
                Node.Cout = cout;

                var x = new Word("x", Tag.ID);
                var id = new Id(x, VarType.INT, 0);
                var num = new Num(12);
                var expr = new Constant(num, VarType.INT);
                var set = new Set(id, expr);
                set.Gen(1, 2);

                Assert.AreEqual("\tx = 12\r\n", cout.ToString());
            }
        }
    }
}