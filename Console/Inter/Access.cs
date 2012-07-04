// -----------------------------------------------------------------------
// <copyright file="Access.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using Lexical;
using Symbols;

namespace Inter
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Access : Op
    {
        public readonly Id array;
        public readonly Expr index;

        public Access(Id a, Expr i, VarType p)
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
            return array + " [ " + index + " ]";
        }
    }
}
