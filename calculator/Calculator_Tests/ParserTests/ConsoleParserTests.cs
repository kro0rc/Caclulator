using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Calculator.Parser;

namespace Calculator_Tests.ParserTests
{
    [TestClass]
    public class ConsoleParserTests
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
            parser = new ConsoleParser();
        }

        [DataTestMethod]
        [DataRow("2+2", true)]
        [DataRow("2+2+2", true)]
        [DataRow("2+2+2*2", true)]
        [DataRow("1.1+2.2/3*-2", true)]
        [DataRow("450000*0-2+12*2.3", true)]
        [DataRow("-2+-3+-4*-5--6", true)]
        [DataRow("2+2+(2*2)", false)]
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
        public void CheckExpressionBeforeParsing_ReturnsTrueIfExpressionIsCorrect(string testExpression, bool expectedResult)
        {
            bool checkResult = parser.CheckExpressionBeforeParsing(testExpression);
            Console.WriteLine(testExpression + " " + checkResult);
            Assert.AreEqual(checkResult, expectedResult);
        }

        [DataTestMethod]
        [DataRow("2+2", 3)]
        [DataRow("2+2+2", 5)]
        [DataRow("2+2+2*2", 7)]
        [DataRow("1.1+2.2/3*-2", 7)]
        [DataRow("450000*0-2+12*2.3", 9)]
        [DataRow("-2+-3+-4*-5--6", 9)]
        [DataRow("15+-5-131", 5)]
        
        public void ParseExpressionTest_ReturnsCorrectlyParsedExpression(string expression, int expectedLength)
        {
            List<string> output = parser.ParseExpression(expression);

            Assert.AreEqual(expectedLength, output.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void GetNestingLevel_ExpectedNotImplementedException()
        {
            int result = parser.GetNestingLevel(string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void GetNestedExpressionIndexes_ExpectedNotImplementedException()
        {
            int[] result = parser.GetNestedExpressionIndexes(string.Empty, new int());
        }

    }
}
