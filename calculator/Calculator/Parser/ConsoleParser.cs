using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Calculator.Parser
{
    public class ConsoleParser : ExpressionParser
    {
        protected override bool IsBracketsCorrect(string expression)
        {
            List<char> availableBrackets = new List<char>() { '(', ')' };

            for (int i = 0; i < expression.Length; i++)
            {
                if(availableBrackets.Contains(expression[i]))
                {
                    System.Console.WriteLine("got bracket");
                    return false;
                }
            }

            return true;
        }
    }
}
