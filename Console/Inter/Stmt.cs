// -----------------------------------------------------------------------
// <copyright file="Stmt.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Inter
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Stmt : Node
    {
        public static readonly Stmt Null = new Stmt();

        public static Stmt Enclosing = Stmt.Null;

        public int after;

        public Stmt()
        { }

        public virtual void Gen(int b, int a)
        { }
    }
}
