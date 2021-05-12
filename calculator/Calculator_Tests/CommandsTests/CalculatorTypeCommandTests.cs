using Microsoft.VisualStudio.TestTools.UnitTesting;
using Calculator.UserInteraction;
using Calculator.Commands;
using Calculator.CalculationProcessor;
using Calculator.Parser;
using System;
using Moq;

namespace Calculator_Tests.CommandsTests
{
    [TestClass]
    public class CalculatorTypeCommandTests
    {
        Mock<IUserInteraction> testInteraction;
        CalculatorTypeCommand calcTypeCommand;
        

        [TestCleanup]
        public void TestCleanUp()
        {
            testInteraction = null;
            calcTypeCommand = null;
        }

        [TestInitialize]
        public void TestInitialize()
        {
            testInteraction = new Mock<IUserInteraction>();
            calcTypeCommand = new CalculatorTypeCommand(testInteraction.Object, null);
        }

        [DataTestMethod]
        [DataRow(ConsoleKey.C)]
        [DataRow(ConsoleKey.F)]
        public void CalculatorTypeCommand_ShouldReturnCorectInstance(ConsoleKey key)
        {
            var calcType = SetCalculatorType(key);
            testInteraction.Setup(x => x.GetUserKey(It.IsAny<string>())).Returns(key);
            calcTypeCommand.Execute();

            Assert.IsInstanceOfType(calcTypeCommand.CalculatorType, calcType);
        }

        private Type SetCalculatorType(ConsoleKey key)
        {
            if(key == ConsoleKey.C)
            {
                return new ConsoleCalculator(new ConsoleInteraction(), new ConsoleParser()).GetType();
            }
            
            return new FileCalculator(new ConsoleInteraction(), new FileExpressionParser(), null).GetType();

        }
    }
}