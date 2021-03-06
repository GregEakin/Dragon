﻿// -----------------------------------------------------------------------
// <copyright file="Program.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace ConsoleX
{
    using System;
    using Lexical;
    using Parser;

    internal class Program
    {
        public static void Main(string[] args)
        {
            var lex = new Lexer();
            var parser = new Parser(lex);
            parser.Program();
            Console.WriteLine();
            Console.ReadLine();
        }
    }
}
