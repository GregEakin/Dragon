// -----------------------------------------------------------------------
// <copyright file="Error.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace ConsoleX
{
    using System;

    public class Error : Exception
    {
        public Error(string msg)
            : base(msg)
        { }
    }
}
