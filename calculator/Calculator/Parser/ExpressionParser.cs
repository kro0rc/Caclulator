using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Globalization;

namespace Calculator.Parser
{
    public abstract class ExpressionParser
    {
        public bool correctStringFormat { get; private set; }
        protected string _numberWithDotRegex = @"^\-?\d+(\.\d{0,})?";
        protected string _operatorRegex = @"^[-+*/]";
        protected Regex _numberWithDot;
        protected Regex _numberWithComma;
        protected Regex _operatorFinder;
        private int _minExpressionLength = 3;
        private List<string> _availableOperators;

        public ExpressionParser()
        {
            this._numberWithDot = new Regex(this._numberWithDotRegex);
            this._operatorFinder = new Regex(this._operatorRegex);
            this._availableOperators = new List<string>() { "-", "+", "*", "/" };
        }

        public bool CheckExpressionBeforeParsing(string expression)
        {
            bool formatIsChecked = CheckFormat(expression);
            bool bracketsAreChecked = CheckBrackets(expression);
            bool operatorsOrderIsChecked = CheckOperatorsOrder(expression);

            if (formatIsChecked && bracketsAreChecked && operatorsOrderIsChecked)
            {
                return true;
            }

            return false;
        }

        public List<string> ParseExpression(string expression)
        {
            return this.ParseSimpleExpression(expression);
        }

        public int[] GetNestedExpressionIndexes(string expression, int currentNestingLevel)
        {
            return this.GetExpressionPartPosition(expression, currentNestingLevel);
        }

        public string PrepareExpressionPart(string expressionPart)
        {
            return this.RemoveBracketsBeforeParsing(expressionPart);
        }

        public int GetNestingLevel(string expression)
        {
            return CountNestingLevel(expression);
        }

        protected virtual bool CheckBrackets(string expression)
        {
            return true;
        }

        protected virtual int CountNestingLevel(string expression)
        {
            return 0;
        }

        protected virtual int[] GetExpressionPartPosition(string expression, int currentNestingLevel)
        {
            return new int[] { };
        }

        protected virtual string RemoveBracketsBeforeParsing(string expressionPart)
        {
            return expressionPart;
        }

        private List<string> ParseSimpleExpression(string expression)
        {

            List<string> expressionParts = new List<string>();
            string modifyedExpression = expression.Replace(" ", "");

            while (modifyedExpression.Length != 0)
            {
                Match numberWithDot = this._numberWithDot.Match(modifyedExpression);
                Match sign = this._operatorFinder.Match(modifyedExpression);

                if (numberWithDot.Success)
                {
                    expressionParts.Add(numberWithDot.Value);
                    modifyedExpression = modifyedExpression.Remove(0, numberWithDot.Value.Length);
                }

                else if (!numberWithDot.Success && sign.Success)
                {
                    expressionParts.Add(sign.Value);
                    modifyedExpression = modifyedExpression.Remove(0, sign.Value.Length);
                }
            }

            return expressionParts;
        }

        private bool CheckFormat(string expression)
        {
            if (expression.Length < this._minExpressionLength || String.IsNullOrWhiteSpace(expression))
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

        private bool CheckOperatorsOrder(string expression)
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

                    if (i <= expression.Length - 2 && expression[i + 1] != availableOperators[0])
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
