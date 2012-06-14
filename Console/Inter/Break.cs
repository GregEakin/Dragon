// -----------------------------------------------------------------------
// <copyright file="Break.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Inter
{

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Break : Stmt
    {
        Stmt stmt;
        public Break()
        {
            if (Stmt.Enclosing == Stmt.Null)
                error("unenclosed  break");
            stmt = Stmt.Enclosing;
        }
        public override void gen(int b, int a)
        {
            emit("goto L" + stmt.after);
        }
    }
}
