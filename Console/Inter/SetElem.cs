// -----------------------------------------------------------------------
// <copyright file="SetElem.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Inter
{
    using Symbols;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class SetElem : Stmt
    {
        public readonly Id array;
        public readonly Expr index;
        public readonly Expr expr;
        
        public SetElem(Access x, Expr y)
        {
            array = x.array;
            index = x.index;
            expr = y;
            if (Check(x.type, expr.type) == null)
                Error("type error");
        }
        
        public SType Check(SType p1, SType p2)
        {
            if (p1 is Array || p2 is Array)
                return null;
            else if (p1 == p2)
                return p2;
            else if (SType.numeric(p1) && SType.numeric(p2))
                return p2;
            else
                return null;
        }

        public override void Gen(int b, int a)
        {
            string s1 = index.Reduce().ToString();
            string s2 = expr.Reduce().ToString();
        }
    }
}
