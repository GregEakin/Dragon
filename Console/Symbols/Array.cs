// -----------------------------------------------------------------------
// <copyright file="Array.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Symbols
{
    using Lexical;

    public class Array : VarType
    {
        public readonly VarType Of;

        public readonly int Size;

        public Array(int sz, VarType p)
            : base("[]", Lexical.Tag.INDEX, sz * p.width)
        {
            Size = sz;
            Of = p;
        }

        public override string ToString()
        {
            return "[" + Size + "] " + Of;
        }
    }
}
