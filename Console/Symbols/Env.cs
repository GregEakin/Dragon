// -----------------------------------------------------------------------
// <copyright file="Enf.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Symbols
{
    using System.Collections.Generic;
    using Inter;
    using Lexical;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Env
    {
        private readonly Dictionary<Token, Id> table = new Dictionary<Token, Id>();
        protected Env prev;
        public Env(Env n) { prev = n; }
        public void put(Token w, Id i) { table.Add(w, i); }
        public Id get(Token w)
        {
            for (Env e = this; e != null; e = e.prev)
            {
                Id found = (Id)(e.table[w]);
                if (found != null)
                    return found;
            }
            return null;
        }
    }
}
