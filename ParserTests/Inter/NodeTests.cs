using System.IO;
using Inter;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConsoleTests.Inter
{
    [TestClass]
    public class NodeTests
    {
        [TestMethod]
        public void NodeEmitLabelTest()
        {
            using (var cout = new StringWriter())
            {
                Node.Cout = cout;

                var node = new Node();
                node.EmitLabel(11);

                var actual = cout.ToString();
                Assert.AreEqual("L11:\r\n", actual);
            }
        }

        [TestMethod]
        public void NodeEmitTest()
        {
            using (var cout = new StringWriter())
            {
                Node.Cout = cout;

                var node = new Node();
                node.Emit("test");

                var actual = cout.ToString();
                Assert.AreEqual("\ttest\r\n", actual);
            }
        }
    }
}