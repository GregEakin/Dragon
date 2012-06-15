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
        static int count;
        int number;

        public Temp(SType p)
            : base(Word.TEMP, p)
        {
            number = ++count;
        }

        public override string ToString()
        {
            return "t" + number;
        }
    }
}
