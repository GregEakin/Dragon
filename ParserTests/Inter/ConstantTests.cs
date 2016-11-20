using System.IO;
using Inter;
using Lexical;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Symbols;

namespace ConsoleTests.Inter
{
    [TestClass]
    public class ConstantTests
    {
        [TestMethod]
        public void ConstantTrueTest()
        {
            using (var cout = new StringWriter())
            {
                Node.Cout = cout;

                var truenode = Constant.TRUE;
                Assert.AreEqual(Word.TRUE, truenode.Op);
                Assert.AreEqual(VarType.BOOL, truenode.Type);
                truenode.Jumping(1, 2);

                Assert.AreEqual("\tgoto L1\r\n", cout.ToString());
            }
        }

        [TestMethod]
        public void ConstantFalseTest()
        {
            using (var cout = new StringWriter())
            {
                Node.Cout = cout;

                var falsenode = Constant.FALSE;
                Assert.AreEqual(Word.FALSE, falsenode.Op);
                Assert.AreEqual(VarType.BOOL, falsenode.Type);
                falsenode.Jumping(1, 2);

                Assert.AreEqual("\tgoto L2\r\n", cout.ToString());
            }
        }

        [TestMethod]
        public void ConstantCtorIntTest()
        {
            using (var cout = new StringWriter())
            {
                Node.Cout = cout;

                var intnode = new Constant(3);
                // Assert.AreEqual(Word.FALSE, intnode.Op);
                Assert.AreEqual(VarType.INT, intnode.Type);
                intnode.Jumping(1, 2);

                // Assert.AreEqual("\tgoto L2\r\n", cout.ToString());
            }
        }

        [TestMethod]
        public void ConstantCtorFloatTest()
        {
            using (var cout = new StringWriter())
            {
                Node.Cout = cout;

                var floatnode = new Constant(3.0f);
                // Assert.AreEqual(new Real(3.0f), floatnode.Op);
                Assert.AreEqual(VarType.FLOAT, floatnode.Type);
                floatnode.Jumping(1, 2);

                //Assert.AreEqual("\tgoto L2\r\n", cout.ToString());
            }
        }
    }
}
