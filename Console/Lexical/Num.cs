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
        public readonly int Value;

        public Num(int v)
            : base(Tag.NUM)
        {
            Value = v;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
