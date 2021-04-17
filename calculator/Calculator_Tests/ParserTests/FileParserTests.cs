using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Calculator.Parser;

namespace Calculator_Tests.ParserTests
{
    [TestClass]
    public class FileParserTests
    {
        ExpressionParser parser;

        [TestCleanup]
        public void TestCleanUp()
        {
            parser = null;
        }

        [TestInitialize]
        public void TestInitialize()
        {
            parser = new FileExpressionParser();
        }

        [DataTestMethod]
        [DataRow("2+2", true)]
        [DataRow("2+2+2", true)]
        [DataRow("2+2+2*2", true)]
        [DataRow("2+2+(2*2)", true)]
        [DataRow("1.1+2.2/3*-2", true)]
        [DataRow("450000*0-2+12*2.3", true)]
        [DataRow("450000*0-2+(12*2.3)+2", true)]
        [DataRow("-2+-3+-4*-5--6", true)]
        [DataRow("2+2+(2*2)", true)]
        [DataRow("-2+-3+-4*-5--()6", false)]
        [DataRow("2+2+2**2", false)]
        [DataRow("2+2+2*2-", false)]
        [DataRow("--2+2+2*2", false)]
        [DataRow("2++2+2*2", false)]
        [DataRow("2+2/0", false)]
        [DataRow("2+2--", false)]
        [DataRow("2+2+", false)]
        [DataRow("test+++++", false)]
        [DataRow(" ", false)]
        [DataRow("", false)]
        [DataRow("2+2*(2+2))", false)]
        [DataRow("2+2*()", false)]
        [DataRow("()", false)]
        [DataRow("2+2*)2+2(", false)]
        public void CheckExpressionBeforeParsing_ReturnsTrueIfExpressionIsCorrect(string testExpression, bool expectedResult)
        {
            bool checkResult = parser.CheckExpressionBeforeParsing(testExpression);
            Console.WriteLine(testExpression + " " + checkResult);
            Assert.AreEqual(expectedResult, checkResult);
        }

        [DataTestMethod]
        [DataRow("2+2")]
        [DataRow("2+2+2")]
        [DataRow("2+2+2*2")]
        [DataRow("1.1+2.2/3*-2")]
        [DataRow("450000*0-2+12*2.3")]
        [DataRow("-2+-3+-4*-5--6")]
        public void ParseExpressionTest_ReturnsCorrectlyParsedExpression(string expression)
        {
            List<string> output = parser.ParseExpression(expression);
            string convertedOutput = String.Join("", output);

            Assert.AreEqual(expression, convertedOutput);
        }

        [DataTestMethod]
        [DataRow("2+2", 0)]
        [DataRow("2+2+2", 0)]
        [DataRow("2+2+2*2", 0)]
        [DataRow("2+2+(2*2)", 1)]
        [DataRow("450000*0-(2+12*(2.3+1))", 2)]
        [DataRow("45*(1+(3+3)+(0-2+12)*2.3)", 3)]
        [DataRow("3*(2+2+2+2+2+2+2+2+0.5*11/12*3--2*(12312312/100/10*(3+3)+1--2)/3)", 3)]
        public void GetNestingLevel_ShouldReturnCorrectValue(string expression, int expectedNestingLevel)
        {
            int actualNestingLevel = parser.GetNestingLevel(expression);
            Console.WriteLine(actualNestingLevel);
            Assert.AreEqual(expectedNestingLevel, actualNestingLevel); 
        }

        [DataTestMethod]
        [DataRow("450000*0-(2+12*(2.3+1))", 15, 21, 2)]
        [DataRow("45*(1+(3+3)+(0-2+12)*2.3)", 12, 19, 3)]
        [DataRow("3*(2+2+2+2+2+2+2+2+0.5*11/12*3--2*(12312312/100/10*(3+3)+1--2)/3)", 51, 55, 3)]
        [DataRow("2+2", 0, 0, 0)]
        public void GetNestedExpressionIndexes_ExpectedNotImplementedException(string expression, int startIndex, int endIndex, int nestingLevel)
        {
            int[] expectedIndexes = new int[] { startIndex, endIndex };
            int[] resultIndexes = parser.GetNestedExpressionIndexes(expression, nestingLevel);

            for(int i = 0; i < expectedIndexes.Length; i++)
            {
                Assert.AreEqual(expectedIndexes[i], resultIndexes[i]);
            }
        }

    }
}
