using System;

namespace QUT
{
    public class SyntaxException : Exception
    {
        public int Row { get; private set; }
        public int Column { get; private set; }

        public SyntaxException(string message) : base(message) { }

        public SyntaxException(string message, int row, int column) : base(message)
        {
            Row = row;
            Column = column;
        }
    }
}
