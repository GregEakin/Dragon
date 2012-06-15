﻿// -----------------------------------------------------------------------
// <copyright file="Not.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Inter
{
    using Lexical;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Not : Logical
    {
        public Not(Token tok, Expr x2)
            : base(tok, x2, x2)
        { }

        public override void Jumping(int t, int f)
        {
            expr2.Jumping(f, t);
        }

        public override string ToString()
        {
            return op.ToString() + " " + expr2.ToString();
        }
    }
}
