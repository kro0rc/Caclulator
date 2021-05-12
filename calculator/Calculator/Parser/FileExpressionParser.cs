using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Calculator.Parser
{
    public class FileExpressionParser : ExpressionParser
    {
        private int[] _bracketsCount;
        private char _openingBracket = '(';
        private char _closingBracket = ')';
        
        protected override bool IsBracketsCorrect(string expression)
        { 
            this._bracketsCount = CountBrackets(expression);
            bool bracketsArePresent = this._bracketsCount[0] > 0 && this._bracketsCount[1] > 0;
            bool bracketsCountIsEqual = this._bracketsCount[0] == this._bracketsCount[1];
            bool openingBracketIsFound = false;
            int lastOpeningBracketPosition = 0;
            int firstClosingBracketPosition = 0;

            if(bracketsArePresent)
            {
                for (int i = 0; i < expression.Length; i++)
                {
                    if (expression[i] == this._openingBracket)
                    {
                        lastOpeningBracketPosition = i;
                        openingBracketIsFound = true;
                    }
                    else if (expression[i] == this._closingBracket && firstClosingBracketPosition == 0)
                    {
                        if (!openingBracketIsFound)
                        {
                            return false;
                        }

                        firstClosingBracketPosition = i;
                    }
                    else if(expression[i] == this._closingBracket && i < expression.Length - 2 && expression[i + 1] == this._openingBracket)
                    {
                        return false;
                    }
                }

                int distanceBeetweenBrackets = firstClosingBracketPosition - lastOpeningBracketPosition;

                if ((Math.Abs(distanceBeetweenBrackets) < 4 && Math.Abs(distanceBeetweenBrackets) != 2) || !bracketsCountIsEqual)
                {
                    return false;
                }

                return true;
            }

            return true;
        }

        protected override int CountNestingLevel(string expression)
        {
            int[] actualBracketsCount = CountBrackets(expression);
            return actualBracketsCount[0];
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

        protected override string RemoveBracketsBeforeParsing(string expressionPart)
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
        
        private int[] CountBrackets(string expression)
        {
            byte openingBracketsCount = 0;
            byte closingBracketsCount = 0;

            for (int i = 0; i < expression.Length; i++)
            {
                if (expression[i] == this._openingBracket)
                {
                    openingBracketsCount++;
                }

                else if (expression[i] == this._closingBracket)
                {
                    closingBracketsCount++;
                }
            }

            return new int[] { openingBracketsCount, closingBracketsCount };
        }
    }
}
