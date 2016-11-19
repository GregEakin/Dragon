// -----------------------------------------------------------------------
// <copyright file="Stmt.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Inter
{
    public class Stmt : Node
    {
        public static Stmt Null { get; } = new Stmt();

        public static Stmt Enclosing { get; set; } = Null;

        public int After { get; set; }

        public virtual void Gen(int b, int a)
        {
        }
    }
}
