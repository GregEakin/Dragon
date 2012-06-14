// -----------------------------------------------------------------------
// <copyright file="Node.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Inter
{
    using System;
    using Lexical;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Node
    {
        int lexline = 0;
        public Node() { lexline = Lexer.line; }
        public void error(string s) { throw new Error("near line " + lexline + ": " + s); }
        static int labels = 0;
        public int newlabel() { return ++labels; }
        public void emitlabel(int i) { Console.Write("L" + i + ":"); }
        public void emit(string s) { Console.Write("\t" + s); }
    }
}
