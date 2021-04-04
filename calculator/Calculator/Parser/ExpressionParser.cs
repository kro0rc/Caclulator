using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Calculator.Parser
{
    public abstract class ExpressionParser
    {
        public List<string> expressionParts { get; private set; }
        public bool correctStringFormat { get; private set; }
        protected string _numberRegEx = @"^\-?\d+(\.\d{0,})?";
        protected string _operatorRegex = @"^[-+*/]";
        protected Regex _numberFinder;
        protected Regex _operatorFinder;
        private int _minExpressionLength = 3;

        public ExpressionParser()
        {
            this._numberFinder = new Regex(this._numberRegEx);
            this._operatorFinder = new Regex(this._operatorRegex);
        }

        public void CheckAndParse(string expression)
        {
            this.correctStringFormat = CheckExpression(expression);
            
            if (correctStringFormat)
            {
                Parse(expression);
            }
        }

        public void Parse(string expression)
        {
            
            this.expressionParts = new List<string>();
            string modifyedExpression = expression.Replace(" ", "");

            while (modifyedExpression.Length != 0)
            {
                Match number = this._numberFinder.Match(modifyedExpression);
                Match sign = this._operatorFinder.Match(modifyedExpression);

                if (number.Success)
                {
                    this.expressionParts.Add(number.Value);
                    modifyedExpression = modifyedExpression.Remove(0, number.Value.Length);
                }

                else if (!number.Success && sign.Success)
                {
                    this.expressionParts.Add(sign.Value);
                    modifyedExpression = modifyedExpression.Remove(0, sign.Value.Length);
                }
            }
        }

        private bool CheckExpression(string expression)
        {
            if (expression.Length > this._minExpressionLength || String.IsNullOrWhiteSpace(expression))
            {
                return false;
            }

            foreach (var item in expression)
            {
                if (Char.IsLetter(item))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
