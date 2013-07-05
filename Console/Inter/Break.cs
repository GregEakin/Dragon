// -----------------------------------------------------------------------
// <copyright file="Break.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using ConsoleX;

namespace Inter
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Break : Stmt
    {
        readonly Stmt stmt;

        public Break()
        {
            if (Stmt.Enclosing == Stmt.Null)
                throw new Error("near line " + lexline + ": unenclosed  break");
            stmt = Stmt.Enclosing;
        }

        public override void Gen(int b, int a)
        {
            Emit("goto L" + stmt.After);
        }
    }
}
