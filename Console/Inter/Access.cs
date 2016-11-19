// -----------------------------------------------------------------------
// <copyright file="Access.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using Lexical;
using Symbols;

namespace Inter
{
    public class Access : Op
    {
        public Id Array { get; }
        public Expr Index { get; }

        public Access(Id a, Expr i, VarType p)
            : base(new Word("[]", Tag.INDEX), p)
        {
            Array = a;
            Index = i;
        }

        public override Expr Gen()
        {
            return new Access(Array, Index.Reduce(), Type);
        }

        public override string ToString()
        {
            return Array + " [ " + Index + " ]";
        }
    }
}
