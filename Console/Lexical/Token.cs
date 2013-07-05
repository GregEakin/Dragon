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
        public readonly int Tag;

        public Token(int tag) 
        { 
            this.Tag = tag;
        }
        
        public override string ToString()
        {
            return ((char)this.Tag).ToString(CultureInfo.InvariantCulture);
        }
    }
}
