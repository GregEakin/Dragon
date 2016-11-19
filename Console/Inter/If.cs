// -----------------------------------------------------------------------
// <copyright file="If.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using ConsoleX;
using Symbols;

namespace Inter
{
    public class If : Stmt
    {
        private readonly Expr _expr;
        private readonly Stmt _stmt;

        public If(Expr x, Stmt s)
        {
            _expr = x;
            _stmt = s;
            if (_expr.Type != VarType.BOOL)
                throw new Error("near line " + _expr.Lexline + ": boolean required in if, not " + _expr.Type);
        }

        public override void Gen(int b, int a)
        {
            var label = NewLabel();
            _expr.Jumping(0, a);
            EmitLabel(label);
            _stmt.Gen(label, a);
        }
    }
}
