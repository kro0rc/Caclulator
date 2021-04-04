using System;
using Calculator.CalculationProcessor;
using Calculator.Parser;

namespace Calculator.Commands
{
    public interface IGetTypeCommand
    {
        public CalculatorRealization CalculatorType { get; }
        public ExpressionParser ParserType { get; }
    }
}
