// -----------------------------------------------------------------------
// <copyright file="Else.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using ConsoleX;
using Symbols;

namespace Inter
{
    public class Else : Stmt
    {
        private readonly Expr _expr;
        private readonly Stmt _stmt1;
        private readonly Stmt _stmt2;

        public Else(Expr x, Stmt s1, Stmt s2)
        {
            _expr = x;
            _stmt1 = s1;
            _stmt2 = s2;
            if (_expr.Type != VarType.BOOL)
                throw new Error("near line " + _expr.Lexline + ": boolean required in if, not " + _expr.Type);
        }

        public override void Gen(int b, int a)
        {
            var label1 = NewLabel();
            var label2 = NewLabel();
            _expr.Jumping(0, label2);
            EmitLabel(label1);
            _stmt1.Gen(label1, a);
            Emit("goto L" + a);
            EmitLabel(label2);
            _stmt2.Gen(label2, a);
        }
    }
}
