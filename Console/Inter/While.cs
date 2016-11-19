// -----------------------------------------------------------------------
// <copyright file="While.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using ConsoleX;
using Symbols;

namespace Inter
{
    public class While : Stmt
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
            _expr.Jumping(0, a);
            var label = NewLabel();
            EmitLabel(label);
            _stmt.Gen(label, b);
            Emit("goto L" + b);
        }
    }
}
