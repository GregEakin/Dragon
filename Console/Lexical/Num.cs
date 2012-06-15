// -----------------------------------------------------------------------
// <copyright file="Num.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Lexical
{

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Num : Token
    {
        public readonly int value;

        public Num(int v)
            : base(Tag.NUM)
        {
            value = v;
        }

        public override string ToString()
        {
            return "" + value;
        }
    }
}
