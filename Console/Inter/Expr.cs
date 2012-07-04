// -----------------------------------------------------------------------
// <copyright file="Expr.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using ConsoleX;
using Lexical;
using Symbols;

namespace Inter
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Expr : Node
    {
        public readonly Token op;
        public readonly VarType type;

        public Expr(Token tok, VarType p)
        {
            op = tok;
            type = p;
            if (type == null)
                throw new Error("near line " + lexline + ": type error");
        }

        public virtual Expr Gen()
        {
            return this;
        }

        public virtual Expr Reduce()
        {
            return this;
        }

        public virtual void Jumping(int t, int f)
        {
            EmitJumps(ToString(), t, f);
        }

        public void EmitJumps(string test, int t, int f)
        {
            if (t != 0 && f != 0)
            {
                Emit("if " + test + " goto L" + t);
                Emit("goto L" + f);
            }
            else if (t != 0) Emit("if " + test + " goto L" + t);
            else if (f != 0) Emit("iffalse " + test + " goto L" + f);
        }

        public override string ToString()
        {
            return op.ToString();
        }
    }
}
