// -----------------------------------------------------------------------
// <copyright file="Type.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using Lexical;

namespace Symbols
{
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
            FLOAT = new VarType("float", Lexical.Tag.BASIC, 8),
            INT = new VarType("int", Lexical.Tag.BASIC, 4),
            CHAR = new VarType("char", Lexical.Tag.BASIC, 2),
            BOOL = new VarType("bool", Lexical.Tag.BASIC, 1);

        public static bool Numeric(VarType p)
        {
            return p == VarType.CHAR || p == VarType.INT || p == VarType.FLOAT;
        }

        public static VarType Max(VarType p1, VarType p2)
        {
            if (!Numeric(p1) || !Numeric(p2))
                return null;
            if (p1 == VarType.FLOAT || p2 == VarType.FLOAT)
                return VarType.FLOAT;
            if (p1 == VarType.INT || p2 == VarType.INT)
                return VarType.INT;
            return VarType.CHAR;
        }
    }
}
