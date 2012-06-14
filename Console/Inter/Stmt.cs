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
        public Stmt() { }
        public static readonly Stmt Null = new Stmt();
        public virtual void gen(int b, int a) { }
        public int after = 0;
        public static Stmt Enclosing = Stmt.Null;
    }
}
