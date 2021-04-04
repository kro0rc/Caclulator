using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Calculator.CalculationProcessor
{
    public abstract class CalculatorRealization
    {
        public double Result { get; private set; }
        private List<string> _expression;
        protected Dictionary<string, Func<double, double, double>> _operations = new Dictionary<string, Func<double, double, double>>
        {
            { "+", (x, y) => x + y },
            { "-", (x, y) => x - y },
            { "*", (x, y) => x * y },
            { "/", (x, y) => x / y },
        };

        protected virtual void CalculateWithinBrackets(List<string> list) { }

        public double SimpleCalculating(List<string> list)
        {
            this.Result = 0;
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
                    Double.TryParse(this._expression[0], out double res);
                    this.Result = res;
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

                this._expression[position] = this._operations[operation](leftOperand, rightOperand).ToString();
                this._expression.RemoveAt(position - 1);
                this._expression.RemoveAt(position);
            }

            return this.Result;
        }
    }
}
