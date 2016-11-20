using System.IO;
using ConsoleX;
using Inter;
using Lexical;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Symbols;

namespace ConsoleTests.Inter
{
    [TestClass]
    public class DoTests
    {
        [TestMethod]
        [ExpectedException(typeof(Error))]
        public void DoNonBoolCtorTest()
        {
            var variable = new Id(new Word("x", Tag.ID), VarType.INT, 0);
            var constant = new Constant(new Num(12), VarType.INT);
            var expresion = new Arith(Word.PLUS, variable, constant);

            var donode = new Do();
            donode.Init(expresion, new Stmt());
        }

        [TestMethod]
        public void DoCtorTest()
        {
            using (var cout = new StringWriter())
            {
                Node.Cout = cout;

                var variable = new Id(new Word("x", Tag.ID), VarType.INT, 0);
                var constant = new Constant(new Num(12), VarType.INT);
                var expresion = new Rel(Word.EQ, variable, constant);

                var donode = new Do();
                donode.Init(expresion, new Stmt());
                donode.Gen(11, 22);

                var actual = cout.ToString();
                Assert.AreEqual("L1:\r\n\tif x == 12 goto L11\r\n", actual);
            }
        }

        [TestMethod]
        public void DoGenTest()
        {
            using (var cout = new StringWriter())
            {
                Node.Cout = cout;

                var variable = new Id(new Word("x", Tag.ID), VarType.INT, 0);
                var constant = new Constant(new Num(12), VarType.INT);
                var expresion = new Rel(Word.EQ, variable, constant);

                var donode = new Do();
                donode.Init(expresion, new Stmt());
                donode.Gen(11, 22);

                var actual = cout.ToString();
                Assert.AreEqual("L1:\r\n\tif x == 12 goto L11\r\n", actual);
            }
        }
    }
}