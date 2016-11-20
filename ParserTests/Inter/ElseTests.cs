using System.IO;
using ConsoleX;
using Inter;
using Lexical;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Symbols;

namespace ConsoleTests.Inter
{
    [TestClass]
    public class ElseTests
    {
        [TestMethod]
        [ExpectedException(typeof(Error))]
        public void ElseNonBoolCtorTest()
        {
            var variable = new Id(new Word("x", Tag.ID), VarType.INT, 0);
            var constant = new Constant(new Num(12), VarType.INT);
            var expresion = new Arith(Word.PLUS, variable, constant);

            var elsenode = new Else(expresion, new Stmt(), new Stmt());
        }

        [TestMethod]
        public void ElseCtorTest()
        {
            using (var cout = new StringWriter())
            {
                Node.Cout = cout;

                var variable = new Id(new Word("x", Tag.ID), VarType.INT, 0);
                var constant = new Constant(new Num(12), VarType.INT);
                var expresion = new Rel(Word.EQ, variable, constant);

                var elsenode = new Else(expresion, new Stmt(), new Stmt());
                elsenode.Gen(11, 22);

                var actual = cout.ToString();
                Assert.AreEqual("\tiffalse x == 12 goto L2\r\nL1:\r\n\tgoto L22\r\nL2:\r\n", actual);
            }
        }
    }
}