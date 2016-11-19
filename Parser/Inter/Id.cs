// -----------------------------------------------------------------------
// <copyright file="Id.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using Lexical;
using Symbols;

namespace Inter
{
    public class Id : Expr
    {
        public int Offset { get; }

        public Id(Word id, VarType p, int b) 
            : base(id, p) 
        { 
            Offset = b; 
        }
    }
}
