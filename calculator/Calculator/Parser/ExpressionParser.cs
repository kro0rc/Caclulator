using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System.Globalization;

namespace Calculator.Parser
{
    public abstract class ExpressionParser
    {
        public bool correctStringFormat { get; private set; }
        protected const string _numberWithDotRegex = @"^\-?\d+(\.\d{0,})?";
        protected const string _operatorRegex = @"^[-+*/]";
        protected readonly Regex _numberWithDot;
        protected readonly Regex _numberWithComma;
        protected readonly Regex _operatorFinder;
        private const int _minExpressionLength = 3;
        private readonly List<string> _availableOperators;

        public ExpressionParser()
        {
            this._numberWithDot = new Regex(_numberWithDotRegex);
            this._operatorFinder = new Regex(_operatorRegex);
            this._availableOperators = new List<string>() { "-", "+", "*", "/" };
        }

        public bool CheckExpressionBeforeParsing(string expression)
        {
            return (IsFormatCorrect(expression) && IsBracketsCorrect(expression) && IsOperatorOrderCorrect(expression));
        }

        public List<string> ParseExpression(string expression)
        {
            return this.ParseSimpleExpression(expression);
        }

        public int[] GetNestedExpressionIndexes(string expression, int currentNestingLevel)
        {
            return this.GetExpressionPartPosition(expression, currentNestingLevel);
        }

        public int GetNestingLevel(string expression)
        {
            return CountNestingLevel(expression);
        }

        public string PrepareExpressionPart(string expressionPart)
        {
            return this.RemoveBracketsBeforeParsing(expressionPart);
        }

        protected virtual bool IsBracketsCorrect(string expression)
        {
            return true;
        }

        protected virtual int CountNestingLevel(string expression)
        {
            throw new NotImplementedException();
        }

        protected virtual int[] GetExpressionPartPosition(string expression, int currentNestingLevel)
        {
            return new int[] { };
        }

        protected virtual string RemoveBracketsBeforeParsing(string expressionPart)
        {
            throw new NotImplementedException();
        }

        private List<string> ParseSimpleExpression(string expression)
        {

            List<string> expressionParts = new List<string>();
            string modifyedExpression = expression.Replace(" ", "");

            while (modifyedExpression.Length != 0)
            {
                Match number = this._numberWithDot.Match(modifyedExpression);
                Match sign = this._operatorFinder.Match(modifyedExpression);

                if(number.Success && sign.Success)
                {
                    Match tempSign = this._operatorFinder.Match(expressionParts.LastOrDefault());

                    if (tempSign.Success || expressionParts.Count == 0)
                    {
                        expressionParts.Add(number.Value);
                        modifyedExpression = modifyedExpression.Remove(0, number.Value.Length);
                    }
                    else
                    {
                        expressionParts.Add(sign.Value);
                        modifyedExpression = modifyedExpression.Remove(0, sign.Value.Length);
                    }
                }
                else
                {
                    if (number.Success)
                    {
                        expressionParts.Add(number.Value);
                        modifyedExpression = modifyedExpression.Remove(0, number.Value.Length);
                    }

                    else if (!number.Success && sign.Success)
                    {
                        expressionParts.Add(sign.Value);
                        modifyedExpression = modifyedExpression.Remove(0, sign.Value.Length);
                    }
                }
            }

            return expressionParts;
        }

        private bool IsFormatCorrect(string expression)
        {
            if (expression.Length < _minExpressionLength || String.IsNullOrWhiteSpace(expression))
            {
                return false;
            }

            else
            {
                for (int i = 0; i < expression.Length; i++)
                {
                    if (Char.IsLetter(expression[i]))
                    {
                        return false;
                    }

                    else if (expression.Length - i >= 2)
                    {
                        if (expression[i] == '/' && expression[i + 1] == '0')
                        {
                            Console.WriteLine("Zero dividing Error");
                            return false;
                        }

                    }
                }
            }

            return true;
        }

        private bool IsOperatorOrderCorrect(string expression)
        {
            int maxOperatorsSequencelength = 2;
            int currentOperatorsSequenceLength = 0;
            List<char> availableOperators = new List<char>() { '-', '+', '*', '/' };

            for (int i = 0; i < expression.Length; i++)
            {
                if (availableOperators.Contains(expression[i]))
                {
                    currentOperatorsSequenceLength = currentOperatorsSequenceLength + 1;

                    if (currentOperatorsSequenceLength > maxOperatorsSequencelength)
                    {
                        return false;
                    }

                    if (i <= expression.Length - 2 && availableOperators.Contains(expression[i + 1]) && expression[i + 1] != availableOperators[0])
                    {
                        return false;
                    }
                }

                currentOperatorsSequenceLength = 0;

            }

            return true;
        }

    }
}
