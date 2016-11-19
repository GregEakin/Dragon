// -----------------------------------------------------------------------
// <copyright file="Type.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using Lexical;

namespace Symbols
{
    public class VarType : Word
    {
        public int Width { get; }

        public VarType(string s, int tag, int w)
            : base(s, tag)
        {
            Width = w;
        }

        public static readonly VarType
            FLOAT = new VarType("float", Lexical.Tag.BASIC, 8),
            INT = new VarType("int", Lexical.Tag.BASIC, 4),
            CHAR = new VarType("char", Lexical.Tag.BASIC, 2),
            BOOL = new VarType("bool", Lexical.Tag.BASIC, 1);

        public static bool Numeric(VarType p)
        {
            return p == CHAR || p == INT || p == FLOAT;
        }

        public static VarType Max(VarType p1, VarType p2)
        {
            if (!Numeric(p1) || !Numeric(p2))
                return null;
            if (p1 == FLOAT || p2 == FLOAT)
                return FLOAT;
            if (p1 == INT || p2 == INT)
                return INT;
            return CHAR;
        }
    }
}
