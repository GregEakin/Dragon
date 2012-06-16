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
        public readonly double Value;

        public Real(double v)
            : base(Tag.REAL)
        {
            Value = v;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
