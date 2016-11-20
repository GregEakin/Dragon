using Inter;
using Lexical;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Symbols;

namespace ConsoleTests.Inter
{
    [TestClass]
    public class IdTests
    {
        [TestMethod]
        public void IdCtorTest()
        {
            var token = new Word("x", Tag.ID);
            var id = new Id(token, VarType.BOOL, 11);

            Assert.AreEqual(11, id.Offset);
            Assert.AreEqual(token, id.Op);
            Assert.AreEqual(VarType.BOOL, id.Type);
        }
    }
}