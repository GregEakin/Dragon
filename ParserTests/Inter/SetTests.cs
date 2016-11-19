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
        public void CtorNumericTest()
        {
            var var = new Word("var", Tag.ID);
            var id = new Id(var, VarType.INT, 0);
            var num = new Num(12);
            var expr = new Constant(num, VarType.INT);
            var set = new Set(id, expr);

            Assert.AreEqual(set.Id, id);
            Assert.AreEqual(set.Expr, expr);
        }

        [TestMethod]
        public void CtorBoolTest()
        {
            var var = new Word("var", Tag.ID);
            var id = new Id(var, VarType.BOOL, 0);
            var expr = new Constant(Word.FALSE, VarType.BOOL);
            var set = new Set(id, expr);

            Assert.AreEqual(set.Id, i);
            Assert.AreEqual(set.Expr, x);
        }

        [TestMethod]
        [ExpectedException(typeof(Error))]
        public void CtorInvalidTest()
        {
            var var = new Word("var", Tag.ID);
            var id = new Id(var, VarType.INT, 0);
            var expr = new Constant(Word.FALSE, VarType.BOOL);
            var set = new Set(id, expr);
        }

        [TestMethod]
        public void GenTest()
        {
            var var = new Word("var", Tag.ID);
            var id = new Id(var, VarType.INT, 0);
            var num = new Num(12);
            var expr = new Constant(num, VarType.INT);
            var set = new Set(id, expr);

            // "var = 12"
            set.Gen(1, 2);
        }
    }
}