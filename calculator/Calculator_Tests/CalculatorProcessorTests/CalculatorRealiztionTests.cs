using Microsoft.VisualStudio.TestTools.UnitTesting;
using Calculator.CalculationProcessor;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator_Tests.CalculatorProcessorTests
{
    public class CalculatorRealiztionTests
    {
        CalculatorRealization calculator;


        [TestCleanup]
        public void TestCleanUp()
        {
            calculator = null;
        }

        [TestInitialize]
        public void TestInitialize()
        {
            calculator = new ConsoleCalculator();
        }

        [TestMethod]
        public void TestMethod()
        {
            
        }
    }
}
