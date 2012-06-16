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
        public readonly float Value;

        public Real(float v)
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
