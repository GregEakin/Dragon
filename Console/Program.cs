// -----------------------------------------------------------------------
// <copyright file="Error.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace ConsoleX
{
    using System;
    using Lexical;
    using Parser;

    class Program
    {
        static void Main(string[] args)
        {
            Lexer lex = new Lexer();
            Parser parser = new Parser(lex);
            parser.program();
            Console.WriteLine();
            Console.ReadLine();
        }
    }
}
