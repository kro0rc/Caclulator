using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator.Parser
{
    class FileExpressionParser : ExpressionParser
    {

        protected int minNumbersQuanity = 2;
        protected int minSignQuanity = 1;
        protected char openingBracket = '(';
        protected char closingBracket = ')';

        protected override bool CheckBrackets(string expression)
        {
            int openingBracketPosition = new int();
            int closingBracketPosition = new int();
            bool openingBracketFound = false;
            bool closingBracketFound = false;

            for (int i = 0; i < expression.Length; i++)
            {
                if (expression[i] == this.openingBracket)
                {
                    openingBracketPosition = i;
                    openingBracketFound = true;
                }
            }
        }









    }
}
