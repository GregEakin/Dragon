// -----------------------------------------------------------------------
// <copyright file="Id.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Inter
{
    using Lexical;
    using Symbols;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Id : Expr
    {
        public readonly int offset;

        public Id(Word id, VarType p, int b) 
            : base(id, p) 
        { 
            offset = b; 
        }
    }
}
