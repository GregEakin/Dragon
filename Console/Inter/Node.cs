// -----------------------------------------------------------------------
// <copyright file="Node.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System;
using Lexical;

namespace Inter
{
    public class Node
    {
        private static int _labels;

        public int Lexline { get; }

        public Node()
        {
            Lexline = Lexer.Line;
        }

        public int NewLabel()
        {
            return ++_labels;
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
