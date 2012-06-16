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
    public class VarType : Word
    {
        public readonly int width;

        public VarType(string s, int tag, int w)
            : base(s, tag)
        {
            width = w;
        }

        public static readonly VarType
            FLOAT = new VarType("float64", Tag.BASIC, 8),
            INT = new VarType("int32", Tag.BASIC, 4),
            CHAR = new VarType("char", Tag.BASIC, 2),
            BOOL = new VarType("bool", Tag.BASIC, 1);

        public static bool numeric(VarType p)
        {
            return p == VarType.CHAR || p == VarType.INT || p == VarType.FLOAT;
        }

        public static VarType max(VarType p1, VarType p2)
        {
            if (!numeric(p1) || !numeric(p2))
                return null;
            else if (p1 == VarType.FLOAT || p2 == VarType.FLOAT)
                return VarType.FLOAT;
            else if (p1 == VarType.INT || p2 == VarType.INT)
                return VarType.INT;
            else
                return VarType.CHAR;
        }
    }
}
