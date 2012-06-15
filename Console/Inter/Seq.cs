// -----------------------------------------------------------------------
// <copyright file="Seq.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Inter
{

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Seq : Stmt
    {
        readonly Stmt stmt1;
        readonly Stmt stmt2;

        public Seq(Stmt s1, Stmt s2)
        {
            stmt1 = s1;
            stmt2 = s2;
        }

        public override void Gen(int b, int a)
        {
            if (stmt1 == Stmt.Null)
                stmt2.Gen(b, a);
            else if (stmt2 == Stmt.Null)
                stmt1.Gen(b, a);
            else
            {
                int label = NewLabel();
                stmt1.Gen(b, label);
                EmitLabel(label);
                stmt2.Gen(label, a);
            }
        }
    }
}
