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
        public VarType Of { get; }

        public int Size { get; }

        public Array(int sz, VarType p)
            : base("[]", Lexical.Tag.INDEX, sz * p.Width)
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
