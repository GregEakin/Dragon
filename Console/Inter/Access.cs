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
        readonly public Id array;
        readonly public Expr index;

        public Access(Id a, Expr i, SType p)
            : base(new Word("[]", Tag.INDEX), p)
        {
            array = a;
            index = i;
        }

        public override Expr Gen()
        {
            return new Access(array, index.Reduce(), type);
        }

        public override string ToString()
        {
            return array.ToString() + " [ " + index.ToString() + " ]";
        }
    }
}
