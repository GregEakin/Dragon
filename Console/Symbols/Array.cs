// -----------------------------------------------------------------------
// <copyright file="Array.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Symbols
{
    using Lexical;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Array : SType
    {
        public SType of;
        public int size = 1;
        public Array(int sz, SType p)
            : base("[]", Tag.INDEX, sz * p.width)
        {
            size = sz;
            of = p;
        }
        public override string ToString()
        {
            return "[" + size + "] " + of.ToString();
        }
    }
}
