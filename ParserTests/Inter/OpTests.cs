using System.IO;
using Inter;
using Lexical;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Symbols;

namespace ConsoleTests.Inter
{
    [TestClass]
    public class OpTests
    {
        [TestMethod]
        public void OpCtorTest()
        {
            var token = new Word("x", Tag.ID);
            var op = new Op(token, VarType.INT);

            Assert.AreSame(token, op.Op);
            Assert.AreSame(VarType.INT, op.Type);
        }

        [TestMethod]
        public void NotReduceTest()
        {
            using (var cout = new StringWriter())
            {
                Node.Cout = cout;

                var token = new Word("x", Tag.ID);
                var op = new Op(token, VarType.INT);
                var exp = op.Reduce();
                Assert.IsInstanceOfType(exp, typeof(Temp));
                Assert.AreEqual(VarType.INT, exp.Type);

                var actual = cout.ToString();
                Assert.AreEqual("\tt1 = x\r\n", actual);
            }
        }
    }
}