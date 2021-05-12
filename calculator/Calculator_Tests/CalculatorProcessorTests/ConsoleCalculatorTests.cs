using Microsoft.VisualStudio.TestTools.UnitTesting;
using Calculator.Parser;
using Calculator.CalculationProcessor;
using Calculator.UserInteraction;
using Moq;

namespace Calculator_Tests.CalculatorProcessorTests
{
    [TestClass]
    public class ConsoleCalculatorTests
    {
        CalculatorRealization calculator;
        Mock<IUserInteraction> interaction;

        [TestCleanup]
        public void TestCleanUp()
        {
            calculator = null;
            interaction = null;
        }

        [TestInitialize]
        public void TestInitialize()
        {
            interaction = new Mock<IUserInteraction>();
            calculator = new ConsoleCalculator(interaction.Object, new ConsoleParser());
        }

        [DataTestMethod]
        [DataRow("2+2", 4)]
        [DataRow("2+2+2", 6)]
        [DataRow("2+2+2*2", 8)]
        [DataRow("-2+-3+-4*-5--6", 21)]
        [DataRow("15+-5-131", -121)]
        public void CalculatorTest_ShouldReturnCorrectAnswer(string expression, int expectedResult)
        {
            interaction.SetupSequence(x => x.GetUserInput(It.IsAny<string>()))
                .Returns(expression)
                .Returns("exit")
                .Returns("exit");

            calculator.Run();

            Assert.AreEqual(expectedResult, calculator.CalculatingResult);
        }
    }
}
