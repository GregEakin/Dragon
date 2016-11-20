using Inter;
using Lexical;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Symbols;

namespace ConsoleTests.Inter
{
    [TestClass]
    public class TempTests
    {
        [TestMethod]
        public void TempCtorTest()
        {
            var temp = new Temp(VarType.INT);
            Assert.AreSame(Word.TEMP, temp.Op);
            Assert.AreSame(VarType.INT, temp.Type);
        }

        [TestMethod]
        public void TempToStringTest()
        {
            var temp = new Temp(VarType.INT);
            Assert.AreEqual("t1", temp.ToString());
        }
    }
}