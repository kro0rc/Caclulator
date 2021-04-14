using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator.FileProcessor
{
    [Serializable]
    class FileFormatException : Exception
    {
        public FileFormatException() : base() { }
        public FileFormatException(string message) : base(message) { }
        public FileFormatException(string message, Exception innerException) : base(message, innerException) { }
    }
}
