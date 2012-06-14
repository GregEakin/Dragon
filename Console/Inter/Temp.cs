// -----------------------------------------------------------------------
// <copyright file="Temp.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using Lexical;
using Symbols;
namespace Inter
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Temp : Expr
    {
        static int count = 0;
        int number = 0;
        public Temp(SType p) : base(Word.temp, p) { number = ++count; }
        public override string ToString()
        {
            return "t" + number;
        }
    }
}
