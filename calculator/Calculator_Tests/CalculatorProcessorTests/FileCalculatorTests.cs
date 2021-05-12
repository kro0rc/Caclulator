using Microsoft.VisualStudio.TestTools.UnitTesting;
using Calculator.UserInteraction;
using Calculator.Parser;
using Calculator.CalculationProcessor;
using System;
using System.IO;
using System.Collections.Generic;

namespace Calculator_Tests.CalculatorProcessorTests
{
    [TestClass]
    public class FileCalculatorTests
    {
        private string filePath;
        private List<string> correctExpressions;
        private List<string> uncorrectExpressions;
        private List<string> expectedResults;
        private CalculatorRealization calculator;
        private IUserInteraction interaction;

        [TestCleanup]
        public void TestCleanUp()
        {
            File.Delete(filePath);
            calculator = null;
            interaction = null;
        }

        [TestInitialize]
        public void TestInitialize()
        {
            filePath = "testFile.txt";
            interaction = new ConsoleInteraction();
            using var creatingEmptyFile = File.CreateText(filePath);
        }

        [TestMethod]
        public void CalculateCorrectexpressions()
        {
            correctExpressions = new List<string>()
            {
                "15+-5-(12*(15+1-(3+2)))",
                "2+2+(3*0*(12/2))",
                "3+(11*12)+(1+1)",
                "25+(13+(1+1)/(10/2)*(2*(2*2)+14.5)+(11/2))"
            };

            expectedResults = new List<string>() {"= -122", "= 4", "= 137", "= 52.5"};

            File.WriteAllLines(filePath, correctExpressions);

            calculator = new FileCalculator(interaction, new FileExpressionParser(), filePath);
            calculator.Run();

            List<string> results = GetExpressionResultFromFile(filePath);

            for(int i = 0; i < correctExpressions.Count; i++)
            {
                Assert.AreEqual(expectedResults[i], results[i]);
            }
        }

        [TestMethod]
        public void CalculateIncorrectExpressions()
        {
            uncorrectExpressions = new List<string>()
            {
                "2/0+(1+1)",
                "15+(6 * 17-(18)/8)((7)97 * 11)",
                "(2*2())",
                "(2+2)(2+2)",
                "1 + (2+3))"
            };

            string expectedResult = "= error in expression";

            File.WriteAllLines(filePath, uncorrectExpressions);

            calculator = new FileCalculator(interaction, new FileExpressionParser(), filePath);
            calculator.Run();

            List<string> results = GetExpressionResultFromFile(filePath);

            for (int i = 0; i < uncorrectExpressions.Count; i++)
            {
                Assert.AreEqual(expectedResult, results[i]);
            }
        }

        private List<string> GetExpressionResultFromFile(string pathToFile)
        {
            string[] lines = File.ReadAllLines(pathToFile);
            List<string> results = new List<string>();

            foreach(string str in lines)
            {
                string temp = str.Substring(str.IndexOf("="));
                results.Add(temp);
                Console.WriteLine(temp);
            }

            return results;
        }
    }
}
