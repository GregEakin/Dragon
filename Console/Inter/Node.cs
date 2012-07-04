// -----------------------------------------------------------------------
// <copyright file="Node.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System;
using Lexical;

namespace Inter
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Node
    {
        private static int labels;

        public readonly int lexline;

        public Node()
        {
            lexline = Lexer.Line;
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
