using System.IO;
using ConsoleX;
using Inter;
using Lexical;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Symbols;

namespace ConsoleTests.Inter
{
    [TestClass]
    public class WhileTests
    {
        [TestMethod]
        [ExpectedException(typeof(Error))]
        public void WhileNonBoolCtorTest()
        {
            var variable = new Id(new Word("x", Tag.ID), VarType.INT, 0);
            var constant = new Constant(new Num(12), VarType.INT);
            var expresion = new Arith(Word.PLUS, variable, constant);

            var whilenode = new While();
            whilenode.Init(expresion, new Stmt());
        }

        [TestMethod]
        public void WhileCtorTest()
        {
            var variable = new Id(new Word("x", Tag.ID), VarType.INT, 0);
            var constant = new Constant(new Num(12), VarType.INT);
            var expresion = new Rel(Word.EQ, variable, constant);

            var whilenode = new While();
            whilenode.Init(expresion, new Stmt());
        }

        [TestMethod]
        public void WhileGenTest()
        {
            using (var cout = new StringWriter())
            {
                Node.Cout = cout;

                var variable = new Id(new Word("x", Tag.ID), VarType.INT, 0);
                var constant = new Constant(new Num(12), VarType.INT);
                var expresion = new Rel(Word.EQ, variable, constant);

                var whilenode = new While();
                whilenode.Init(expresion, new Stmt());
                whilenode.Gen(11, 22);

                var actual = cout.ToString();
                Assert.AreEqual("\tiffalse x == 12 goto L22\r\nL1:\r\n\tgoto L11\r\n", actual);
            }
        }
    }
}