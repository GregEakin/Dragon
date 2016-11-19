// -----------------------------------------------------------------------
// <copyright file="Node.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.IO;
using Lexical;

namespace Inter
{
    public class Node
    {
        private static int _labels;

        public static TextWriter Cout { get; set; } = Console.Out;

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
            Cout.WriteLine("L" + i + ":");
        }

        public void Emit(string s)
        {
            Cout.WriteLine("\t" + s);
        }
    }
}
