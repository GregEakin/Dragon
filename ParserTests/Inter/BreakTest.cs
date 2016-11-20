using System.IO;
using ConsoleX;
using Inter;
using Lexical;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Symbols;

namespace ConsoleTests.Inter
{
    [TestClass]
    public class BreakTest
    {
        [TestMethod]
        [ExpectedException(typeof(Error))]
        public void BreakNullCtorTest()
        {
            Stmt.Enclosing = Stmt.Null;
            var @break = new Break();
        }

        [TestMethod]
        public void BreakCtorTest()
        {
            Stmt.Enclosing = new Stmt();
            var @break = new Break();
        }


        [TestMethod]
        public void BreakGenTest()
        {
            using (var cout = new StringWriter())
            {
                Node.Cout = cout;

                var variable = new Id(new Word("x", Tag.ID), VarType.INT, 0);
                var constant = new Constant(new Num(12), VarType.INT);
                var expresion = new Rel(Word.EQ, variable, constant);

                var whilenode = new While();
                whilenode.Init(expresion, new Stmt());

                Stmt.Enclosing = whilenode;
                var breaknode = new Break();
                breaknode.Gen(11, 22);

                var actual = cout.ToString();
                Assert.AreEqual("\tgoto L0\r\n", actual);
            }
        }
    }
}