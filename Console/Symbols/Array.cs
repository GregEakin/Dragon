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
    public class Array : VarType
    {
        public readonly VarType of;
        
        public readonly int size;

        public Array(int sz, VarType p)
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
