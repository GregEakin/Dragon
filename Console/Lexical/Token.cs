// -----------------------------------------------------------------------
// <copyright file="tOKEN.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System.Globalization;

namespace Lexical
{
    public class Token
    {
        public int Tag { get; }

        public Token(int tag) 
        { 
            Tag = tag;
        }
        
        public override string ToString()
        {
            return ((char)Tag).ToString(CultureInfo.InvariantCulture);
        }
    }
}
