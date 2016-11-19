// -----------------------------------------------------------------------
// <copyright file="Error.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace ConsoleX
{
    public class Error : Exception
    {
        public Error(string msg)
            : base(msg)
        { }
    }
}
