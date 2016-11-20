using System.IO;
using Inter;
using Lexical;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Symbols;

namespace ConsoleTests.Inter
{
    [TestClass]
    public class SeqTests
    {
        [TestMethod]
        public void SeqCtorTest()
        {
            var variable = new Id(new Word("x", Tag.ID), VarType.INT, 0);
            var constant = new Constant(new Num(12), VarType.INT);
            var set = new Set(variable, constant);

            var stmt = new Seq(set, set);
        }

        [TestMethod]
        public void SeqGenTest()
        {
            using (var cout = new StringWriter())
            {
                Node.Cout = cout;

                var variable = new Id(new Word("x", Tag.ID), VarType.INT, 0);
                var constant = new Constant(new Num(12), VarType.INT);
                var set = new Set(variable, constant);
                var seq = new Seq(set, set);
                seq.Gen(11, 22);

                var actual = cout.ToString();
                Assert.AreEqual("\tx = 12\r\nL1:\r\n\tx = 12\r\n", actual);
            }
        }
    }
}