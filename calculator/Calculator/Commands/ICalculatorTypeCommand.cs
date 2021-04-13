using System;
using Calculator.CalculationProcessor;
using Calculator.Parser;

namespace Calculator.Commands
{
    public interface ICalculatorTypeCommand
    {
        public CalculatorRealization CalculatorType { get; }
    }
}
