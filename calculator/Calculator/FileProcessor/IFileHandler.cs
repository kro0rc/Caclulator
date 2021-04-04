using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator.FileProcessor
{
    public interface IFileHandler
    {
        bool CheckPath(string path);
        string[] GetLinesFromFile(string path);
        void WriteDataToFile(string[] expressionsArray, string path);
    }
}
