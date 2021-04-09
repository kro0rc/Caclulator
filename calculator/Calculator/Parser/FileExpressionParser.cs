using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Calculator.Parser
{
    class FileExpressionParser : ExpressionParser
    {
        private int _nestingLevel;
        private char _openingBracket = '(';
        private char _closingBracket = ')';
        protected override bool CheckBrackets(string expression)
        {
            byte openingBracketsCount = 0;
            byte closingBracketsCount = 0;

            for (int i = 0; i < expression.Length; i++)
            {
                if(expression[i] == this._openingBracket)
                {
                    openingBracketsCount++;
                }
                else if(expression[i] == this._closingBracket)
                {
                    closingBracketsCount++;
                }
            }

            if (openingBracketsCount == closingBracketsCount)
            {
                this._nestingLevel = openingBracketsCount;
                return true;
            }

            return false;
        }

        protected override int CountNestingLevel(string expression)
        {
            return this._nestingLevel;
        }

        protected override int[] GetExpressionPartPosition(string expression, int currentNestingLevel)
        {
            bool openingBracketIsFound = false;
            int openingBracketposition = 0;
            int closingBracketPosition = 0;
            int bracketsCounter = 0;

            for (int i = 0; i < expression.Length; i++)
            {
                if (!openingBracketIsFound)
                {
                    if (expression[i] == this._openingBracket)
                    {
                        bracketsCounter = bracketsCounter + 1;
                    }

                    if (bracketsCounter == currentNestingLevel)
                    {
                        openingBracketposition = i;
                        openingBracketIsFound = true;
                    }
                }

                else if(openingBracketIsFound)
                {
                    if (expression[i] == this._closingBracket)
                    {
                        closingBracketPosition = i;
                        break;
                    }
                } 
            }

            return new int[] { openingBracketposition, closingBracketPosition };
        }

        protected override string RemoveBracketsBeforeParing(string expressionPart)
        {
            string cleanedExpressionPart = expressionPart;

            for(int i = 0; i < cleanedExpressionPart.Length; i++)
            {
                if (cleanedExpressionPart[i] == this._openingBracket)
                {
                    cleanedExpressionPart = cleanedExpressionPart.Remove(i, 1);
                }

                if (cleanedExpressionPart[i] == this._closingBracket)
                {
                    cleanedExpressionPart = cleanedExpressionPart.Remove(i, 1);
                }
            }

            return cleanedExpressionPart;
        }








    }
}
