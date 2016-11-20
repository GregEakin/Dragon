using System.IO;
using Inter;
using Lexical;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Symbols;

namespace ConsoleTests.Inter
{
    [TestClass]
    public class UnaryTests
    {
        [TestMethod]
        public void UnaryCtorTest()
        {
            var token = new Token('-');
            var variable = new Id(new Word("x", Tag.ID), VarType.INT, 0);
            var unary = new Unary(token, variable);

            Assert.AreSame(token, unary.Op);
            Assert.AreSame(VarType.INT, unary.Type);
        }

        [TestMethod]
        public void UnaryGenTest()
        {
            var token = new Token('-');
            var variable = new Id(new Word("x", Tag.ID), VarType.INT, 0);
            var unary = new Unary(token, variable);
            var exp = unary.Gen();
            Assert.IsInstanceOfType(exp, typeof(Unary));
            Assert.AreEqual(VarType.INT, exp.Type);
        }

        [TestMethod]
        public void UnaryToStringTest()
        {
            var token = new Token('-');
            var variable = new Id(new Word("x", Tag.ID), VarType.INT, 0);
            var unary = new Unary(token, variable);

            Assert.AreEqual("- x", unary.ToString());
        }
    }
}