using Inter;
using Lexical;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Symbols;

namespace ConsoleTests.Inter
{
    [TestClass]
    public class AccessTests
    {
        [TestMethod]
        public void AccessCtorTest()
        {
            var variable = new Id(new Word("x", Tag.ID), VarType.INT, 0);
            var expression = new Expr(new Token(32), VarType.INT);
            var access = new Access(variable, expression, VarType.INT);

            Assert.AreSame(variable, access.Array);
            Assert.AreSame(expression, access.Index);

            var x = access.Gen();
            // Assert.AreEqual("", x.ToString());

            var actual = access.ToString();
            // Assert.AreEqual("  ", actual);
        }
    }
}