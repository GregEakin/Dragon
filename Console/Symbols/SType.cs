// -----------------------------------------------------------------------
// <copyright file="Type.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Symbols
{
    using Lexical;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class SType : Word
    {
        public readonly int width;

        public SType(string s, int tag, int w)
            : base(s, tag)
        {
            width = w;
        }

        public static readonly SType
            Int = new SType("int", Tag.BASIC, 4),
            Float = new SType("float", Tag.BASIC, 8),
            Char = new SType("char", Tag.BASIC, 1),
            Bool = new SType("bool", Tag.BASIC, 1);

        public static bool numeric(SType p)
        {
            return p == SType.Char || p == SType.Int || p == SType.Float;
        }

        public static SType max(SType p1, SType p2)
        {
            if (!numeric(p1) || !numeric(p2))
                return null;
            else if (p1 == SType.Float || p2 == SType.Float)
                return SType.Float;
            else if (p1 == SType.Int || p2 == SType.Int)
                return SType.Int;
            else
                return SType.Char;
        }
    }
}
