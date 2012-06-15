// -----------------------------------------------------------------------
// <copyright file="Node.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Inter
{
    using System;
    using ConsoleX;
    using Lexical;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Node
    {
        static int labels;

        readonly int lexline;

        public Node()
        {
            lexline = Lexer.line;
        }

        public void Error(string s)
        {
            throw new Error("near line " + lexline + ": " + s);
        }

        public int NewLabel()
        {
            return ++labels;
        }

        public void EmitLabel(int i)
        {
            Console.Write("L" + i + ":");
        }

        public void Emit(string s)
        {
            Console.WriteLine("\t" + s);
        }
    }
}
