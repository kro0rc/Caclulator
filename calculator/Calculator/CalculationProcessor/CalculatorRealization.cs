using System;
using System.Collections.Generic;
using System.Globalization;
using Calculator.Commands;

namespace Calculator.CalculationProcessor
{
    public abstract class CalculatorRealization
    {
        public double CalculatingResult { get; private set; }
        private List<string> _expression;
        protected Dictionary<string, Func<double, double, double>> _operations = new Dictionary<string, Func<double, double, double>>
        {
            { "+", (x, y) => x + y },
            { "-", (x, y) => x - y },
            { "*", (x, y) => x * y },
            { "/", (x, y) => x / y },
        };

        protected double SimpleCalculating(List<string> list)
        {
            this.CalculatingResult = 0;
            bool calculated = false;
            this._expression = list;
            

            while (!calculated)
            {
                if (this._expression.Contains("*") || this._expression.Contains("/"))
                {
                    for (int i = 0; i < this._expression.Count; i++)
                    {
                        if (this._expression[i] == "*")
                        {
                            PerformOperation(this._expression[i], i);
                            i = 0;

                        }
                        else if (this._expression[i] == "/")
                        {
                            PerformOperation(this._expression[i], i);
                            i = 0;
                        }
                    }
                }

                if (this._expression.Contains("-") || this._expression.Contains("+"))
                {
                    for (int i = 0; i < this._expression.Count; i++)
                    {
                        if (this._expression[i] == "-")
                        {
                            PerformOperation(this._expression[i], i);
                        }
                        else if (this._expression[i] == "+")
                        {
                            PerformOperation(this._expression[i], i);
                        }
                    }
                }

                if (this._expression.Count == 1)
                {
                    calculated = true;
                    Double.TryParse(this._expression[0], NumberStyles.Number, CultureInfo.InvariantCulture, out double res);
                    this.CalculatingResult = res;
                }
            }

            void PerformOperation(string operation, int position)
            {
                if (!_operations.ContainsKey(operation))
                {
                    throw new ArgumentException(string.Format("Operation {0} is invalid", operation), "op");
                }

                Double.TryParse(this._expression[position - 1], NumberStyles.Number, CultureInfo.InvariantCulture, out double leftOperand);
                Double.TryParse(this._expression[position + 1], NumberStyles.Number, CultureInfo.InvariantCulture, out double rightOperand);

                this._expression[position] = this._operations[operation](leftOperand, rightOperand).ToString(CultureInfo.InvariantCulture);
                this._expression.RemoveAt(position - 1);
                this._expression.RemoveAt(position);
            }

            return this.CalculatingResult;
        }

        public abstract void Run();

        protected void ShowResponse(ICommand command) 
        {
            command.Execute();
        }

        protected bool CheckParsedExpression(List<string> expression)
        {
            int numbersCount = 0;
            int signsCount = 0;
            List<string> availableOperators = new List<string>() { "-", "+", "*", "/" };

            for (int i = 0; i < expression.Count; i++)
            {
                if (Double.TryParse(expression[i], NumberStyles.Number, CultureInfo.InvariantCulture, out double res))
                {
                    numbersCount++;
                }
                else if (availableOperators.Contains(expression[i]))
                {
                    signsCount++;
                }
            }

            if (numbersCount == 2 && signsCount > 0 && signsCount <= 3)
            {
                return true;
            }

            return false;
        }
    }
}
