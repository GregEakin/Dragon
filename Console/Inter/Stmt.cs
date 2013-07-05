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

        private static Stmt enclosing = Stmt.Null;

        public static Stmt Enclosing
        {
            get
            {
                return enclosing;
            }

            set
            {
                enclosing = value;
            }
        }

        public int After { get; set; }

        public virtual void Gen(int b, int a)
        {
        }
    }
}
