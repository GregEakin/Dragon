// -----------------------------------------------------------------------
// <copyright file="Temp.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using Lexical;
using Symbols;
namespace Inter
{
    public class Temp : Expr
    {
        private static int _count;
        private readonly int _number;

        public Temp(VarType p)
            : base(Word.TEMP, p)
        {
            _number = ++_count;
        }

        public override string ToString()
        {
            return "t" + _number;
        }
    }
}
