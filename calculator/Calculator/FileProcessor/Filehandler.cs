using System;
using System.IO;
using System.IO.Packaging;
using Calculator.Commands;
using Calculator.UserInteraction;

namespace Calculator.FileProcessor
{
    public class FileHandler : IFileHandler
    {
        public bool CheckPath(string path)
        {

            if (!File.Exists(path))
            {
                return false;
            }

            return true;
        }

        public string[] GetLinesFromFile(string path)
        {
            string fileExtention = Path.GetExtension(path);

            if (fileExtention != ".txt")
            {
                throw new FileFormatException("Wrong file format! Must be .txt");
            }

            string[] lines = File.ReadAllLines(path);

            return lines;
        }

        public void WriteDataToFile(string[] expressionsArray, string path)
        {
            File.WriteAllLines(path, expressionsArray);
        }

        
    }
}
