// -----------------------------------------------------------------------
// <copyright file="Enf.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using Inter;
using Lexical;

namespace Symbols
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Env
    {
        private readonly Dictionary<Token, Id> table = new Dictionary<Token, Id>();

        protected readonly Env prev;

        public Env(Env n)
        {
            prev = n;
        }

        public void put(Token w, Id i)
        {
            table.Add(w, i);
        }

        public Id get(Token w)
        {
            for (Env e = this; e != null; e = e.prev)
            {
                if (e.table.ContainsKey(w))
                    return (Id)e.table[w];
            }
            return null;
        }
    }
}
