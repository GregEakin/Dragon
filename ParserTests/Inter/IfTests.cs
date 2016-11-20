using System.IO;
using ConsoleX;
using Inter;
using Lexical;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Symbols;

namespace ConsoleTests.Inter
{
    [TestClass]
    public class IfTests
    {
        [TestMethod]
        [ExpectedException(typeof(Error))]
        public void IfNonBoolCtorTest()
        {
            var variable = new Id(new Word("x", Tag.ID), VarType.INT, 0);
            var constant = new Constant(new Num(12), VarType.INT);
            var expresion = new Arith(Word.PLUS, variable, constant);

            var ifnode = new If(expresion, new Stmt());
        }

        [TestMethod]
        public void IfCtorTest()
        {
            var variable = new Id(new Word("x", Tag.ID), VarType.INT, 0);
            var constant = new Constant(new Num(12), VarType.INT);
            var expresion = new Rel(Word.EQ, variable, constant);

            var ifnode = new If(expresion, new Stmt());
        }

        [TestMethod]
        public void IfGenTest()
        {
            using (var cout = new StringWriter())
            {
                Node.Cout = cout;

                var variable = new Id(new Word("x", Tag.ID), VarType.INT, 0);
                var constant = new Constant(new Num(12), VarType.INT);
                var expresion = new Rel(Word.EQ, variable, constant);

                var ifnode = new If(expresion, new Stmt());
                ifnode.Gen(11, 22);

                var actual = cout.ToString();
                Assert.AreEqual("\tiffalse x == 12 goto L22\r\nL1:\r\n", actual);
            }
        }
    }
}