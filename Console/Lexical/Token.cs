// -----------------------------------------------------------------------
// <copyright file="tOKEN.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Lexical
{

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Token
    {
        public readonly int tag;

        public Token(int t) 
        { 
            tag = t; 
        }
        
        public override string ToString()
        {
            return "" + (char)tag;
        }
    }
}
