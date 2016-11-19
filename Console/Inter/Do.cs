// -----------------------------------------------------------------------
// <copyright file="Do.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using ConsoleX;
using Symbols;

namespace Inter
{
    public class Do : Stmt
    {
        private Expr _expr;
        private Stmt _stmt;

        public void Init(Expr x, Stmt s)
        {
            _expr = x;
            _stmt = s;
            if (_expr.Type != VarType.BOOL)
                throw new Error("near line " + _expr.Lexline + ": boolean required in while, not " + _expr.Type);
        }

        public override void Gen(int b, int a)
        {
            After = a;
            var label = NewLabel();
            _stmt.Gen(b, label);
            EmitLabel(label);
            _expr.Jumping(b, 0);
        }
    }
}
