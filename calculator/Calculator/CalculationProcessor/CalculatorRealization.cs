using System;
using System.Collections.Generic;
using System.Globalization;
using Calculator.UserInteraction;
using Calculator.Commands;
using Calculator.Parser;
using Calculator.FileProcessor;


namespace Calculator.CalculationProcessor
{
    public abstract class CalculatorRealization
    {
        public double CalculatingResult { get; private set; }
        protected IUserInteraction _userInteraction;
        protected ExpressionParser _parser;
        protected IFileHandler _fileHandler;
        protected Dictionary<string, Func<double, double, double>> _operations = new Dictionary<string, Func<double, double, double>>
        {
            { "+", (x, y) => x + y },
            { "-", (x, y) => x - y },
            { "*", (x, y) => x * y },
            { "/", (x, y) => x / y },
        };
        private readonly string[,] _operators = new string[,] { { "*", "/" }, { "+", "-" } };
        private List<string> _expression;

        public abstract void Run();

        protected double SimpleCalculating(List<string> list)
        {
            this.CalculatingResult = 0;
            this._expression = list;

            for(int i = 0; i < this._operators.GetLength(0); i++)
            {
                string[] tempOperators = new string[2];

                for(int k = 0; k < this._operators.GetLength(1); k++)
                {
                    tempOperators[k] = this._operators[i, k];
                }

                GetExpressionPart(tempOperators);
            }

            if (this._expression.Count == 1)
            {
                Double.TryParse(this._expression[0], NumberStyles.Number, CultureInfo.InvariantCulture, out double res);
                this.CalculatingResult = res;
            }

            return this.CalculatingResult;
        }

        protected void PerformOperation(string operation, int position)
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

                    if (i <= expression.Count - 2 && availableOperators.Contains(expression[i + 1]) && expression[i + 1] != availableOperators[3])
                    {
                        return false;
                    }
                }
                
            }

            if (numbersCount >= 2 && signsCount > 0 && signsCount <= 3)
            {
                return true;
            }

            return false;
        }

        private void GetExpressionPart(string[] operatorSigns)
        {
            for(int i = 0; i < this._expression.Count; i++)
            {
                for(int k = 0; k < operatorSigns.Length; k++)
                {
                    if (this._expression[i] == operatorSigns[k])
                    {
                        PerformOperation(this._expression[i], i);
                        i = 0;
                        k = 0;
                    }
                }
            }
        }
    }
}
