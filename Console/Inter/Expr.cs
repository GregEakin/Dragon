// -----------------------------------------------------------------------
// <copyright file="Expr.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Inter
{
    using Lexical;
    using Symbols;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Expr : Node
    {
        public Token op;
        public SType type;
        public Expr(Token tok, SType p) { op = tok; type = p; }
        public virtual Expr gen() { return this; }
        public virtual Expr reduce() { return this; }
        public virtual void jumping(int t, int f) { emitjumps(ToString(), t, f); }
        public void emitjumps(string test, int t, int f)
        {
            if (t != 0 && f != 0)
            {
                emit("if " + test + " goto L" + t);
                emit("goto L" + f);
            }
            else if (t != 0) emit("if " + test + " goto L" + t);
            else if (f != 0) emit("iffalse " + test + "goto L" + f);
        }
        public override string ToString()
        {
            return op.ToString();
        }
    }
}
