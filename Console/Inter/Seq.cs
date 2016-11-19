// -----------------------------------------------------------------------
// <copyright file="Seq.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Inter
{

    public class Seq : Stmt
    {
        private readonly Stmt _stmt1;
        private readonly Stmt _stmt2;

        public Seq(Stmt s1, Stmt s2)
        {
            _stmt1 = s1;
            _stmt2 = s2;
        }

        public override void Gen(int b, int a)
        {
            if (_stmt1 == Null)
                _stmt2.Gen(b, a);
            else if (_stmt2 == Null)
                _stmt1.Gen(b, a);
            else
            {
                var label = NewLabel();
                _stmt1.Gen(b, label);
                EmitLabel(label);
                _stmt2.Gen(label, a);
            }
        }
    }
}
