// -----------------------------------------------------------------------
// <copyright file="Break.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using ConsoleX;

namespace Inter
{
    public class Break : Stmt
    {
        private readonly Stmt _stmt;

        public Break()
        {
            if (Enclosing == Null)
                throw new Error("near line " + Lexline + ": unenclosed  break");
            _stmt = Enclosing;
        }

        public override void Gen(int b, int a)
        {
            Emit("goto L" + _stmt.After);
        }
    }
}
