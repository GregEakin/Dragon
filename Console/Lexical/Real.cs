// -----------------------------------------------------------------------
// <copyright file="Real.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Lexical
{

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Real : Token
    {
        public readonly float value;

        public Real(float v)
            : base(Tag.REAL)
        {
            value = v;
        }

        public override string ToString()
        {
            return "" + value;
        }
    }
}
