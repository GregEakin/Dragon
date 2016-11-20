using ConsoleX;
using Inter;
using Lexical;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Symbols;
using System.IO;

namespace ConsoleTests.Inter
{
    [TestClass]
    public class SetTests
    {
        [TestMethod]
        public void SetCtorNumericTest()
        {
            var variable = new Id(new Word("x", Tag.ID), VarType.INT, 0);
            var constant = new Constant(new Num(12), VarType.INT);
            var set = new Set(variable, constant);

            Assert.AreEqual(variable, set.Id);
            Assert.AreEqual(constant, set.Expr);
        }

        [TestMethod]
        public void SetCtorBoolTest()
        {
            var variable = new Id(new Word("x", Tag.ID), VarType.BOOL, 0);
            var constant = new Constant(Word.FALSE, VarType.BOOL);
            var set = new Set(variable, constant);

            Assert.AreEqual(variable, set.Id);
            Assert.AreEqual(constant, set.Expr);
        }

        [TestMethod]
        [ExpectedException(typeof(Error))]
        public void SetCtorInvalidTest()
        {
            var variable = new Id(new Word("x", Tag.ID), VarType.INT, 0);
            var constant = new Constant(Word.FALSE, VarType.BOOL);
            var set = new Set(variable, constant);
        }

        [TestMethod]
        public void SetGenTest()
        {
            using (var cout = new StringWriter())
            {
                Node.Cout = cout;

                var variable = new Id(new Word("x", Tag.ID), VarType.INT, 0);
                var constant = new Constant(new Num(12), VarType.INT);
                var set = new Set(variable, constant);
                set.Gen(1, 2);

                Assert.AreEqual("\tx = 12\r\n", cout.ToString());
            }
        }
    }
}