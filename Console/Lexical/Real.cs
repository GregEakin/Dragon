// -----------------------------------------------------------------------
// <copyright file="Real.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Lexical
{
    public class Real : Token
    {
        public readonly float Value;

        public Real(float v)
            : base(Lexical.Tag.REAL)
        {
            Value = v;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
