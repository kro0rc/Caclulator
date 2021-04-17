using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using Calculator.FileProcessor;

namespace Calculator_Tests.FileProcessorTests
{
    [TestClass]
    public class FileProcessorTests
    {

        IFileHandler fileHandler = new FileHandler();
        string fileName;

        [TestCleanup]
        public void TestCleanUp()
        {
            File.Delete(fileName);
        }

        [TestInitialize]
        public void TestInitialization()
        {
            fileName = "testFile.txt";
            using var creatingEmptyFile = File.CreateText(fileName);
        }

        [TestMethod]
        public void GetLinesFromFile_EmptyFile_EmptyStringArray()
        {
            string[] actual = fileHandler.GetLinesFromFile(fileName);
            Assert.AreEqual(0, actual.Length);
        }

        [TestMethod]
        public void GetLinesFromFile_FilledFile_StringArray()
        {
            string[] linesToWrite = new string[] { "11, 12, 13, 14", "a, b, c, d" };

            File.WriteAllLines(fileName, linesToWrite);

            string[] expected = { "11, 12, 13, 14", "a, b, c, d" };
            string[] actual = fileHandler.GetLinesFromFile(fileName);

            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], actual[i]);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(FileFormatException))]
        public void GetLinesFromFile_FileWithWrongExtension_Exception()
        {
            string fileNameWithIncorrectExtension = "testFile.doc";
            using var cretingFileWithWrongExt = File.Create(fileNameWithIncorrectExtension);
            var result = fileHandler.GetLinesFromFile(fileNameWithIncorrectExtension);
            File.Delete("testFile.doc");
        }

    }

}
