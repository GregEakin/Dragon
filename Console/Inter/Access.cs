// -----------------------------------------------------------------------
// <copyright file="Access.cs" company="">
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
    public class Access : Op
    {
        public Id array;
        public Expr index;
        public Access(Id a, Expr i, SType p)
            : base(new Word("[]", Tag.INDEX), p)
        {
            array = a;
            index = i;
        }
        public override Expr gen() { return new Access(array, index.reduce(), type); }
        public override string ToString()
        {
            return array.ToString() + " [ " + index.ToString() + " ]";
        }
    }
}
