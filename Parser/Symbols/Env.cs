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
    public class Env
    {
        private readonly Dictionary<Token, Id> _table = new Dictionary<Token, Id>();

        protected Env Prev { get; }

        public Env(Env n)
        {
            Prev = n;
        }

        public void Put(Token w, Id i)
        {
            _table.Add(w, i);
        }

        public Id Get(Token w)
        {
            for (var e = this; e != null; e = e.Prev)
            {
                if (e._table.ContainsKey(w))
                    return (Id)e._table[w];
            }

            return null;
        }
    }
}
