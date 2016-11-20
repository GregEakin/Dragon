using System.IO;
using ConsoleX;
using Inter;
using Lexical;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Symbols;

namespace ConsoleTests.Inter
{
    [TestClass]
    public class SetElemTests
    {
        [TestMethod]
        public void SetElemCtorTest()
        {
            var variable = new Id(new Word("x", Tag.ID), VarType.INT, 0);
            var constant = new Constant(new Num(12), VarType.INT);

            // var access = new Access(a, loc, type);
            // var expression = new Expr(token, type);
            // var setElem = new SetElem(access, y);

            // Assert.AreEqual(access.Index, );
        }
    }
}