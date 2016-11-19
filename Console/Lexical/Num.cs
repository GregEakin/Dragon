// -----------------------------------------------------------------------
// <copyright file="Num.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Lexical
{
    public class Num : Token
    {
        public int Value { get; }

        public Num(int v)
            : base(Lexical.Tag.NUM)
        {
            Value = v;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
