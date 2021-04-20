﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Calculator.Parser;
using Calculator.CalculationProcessor;
using Calculator.Commands;
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
            calculator = new ConsoleCalculator();
            interaction = new Mock<IUserInteraction>();
        }

        [TestMethod]
        public void TestMethod()
        {
            interaction.Setup(x => x.GetUserInput(It.IsAny<string>())).Returns("2+2");
            calculator.Run();
        }
    }
}
