using System;
using Calculator.UserInteraction;
using Calculator.Parser;
using Calculator.Commands;
using System.Collections.Generic;

namespace Calculator.CalculationProcessor
{
    class ConsoleCalculator : CalculatorRealization
    {
        private IUserInteraction _userInteraction;
        private ExpressionParser _parser;

        public ConsoleCalculator()
        {
            this._userInteraction = new ConsoleInteraction();
            this._parser = new ConsoleParser();
        }

        public override void Run()
        {
            string userInput = GetUserExpression(new GetUserInputCommand(this._userInteraction));
            bool expressionHasCorrectFormat = this._parser.CheckExpressionBeforeParsing(userInput);

            if (expressionHasCorrectFormat)
            {
                List<string> parsedExpression = this._parser.ParseExpression(userInput);
                bool parsedExpressionIsValid = CheckParsedExpression(parsedExpression);

                if (parsedExpressionIsValid)
                {
                    double result = base.SimpleCalculating(parsedExpression);
                    base.ShowResponse(new ShowResponseCommand(this._userInteraction, result.ToString()));
                }
                else
                {
                    base.ShowResponse(new ShowResponseCommand(this._userInteraction, MessagesTemplates.WarnIncorrectInput));
                }
            }

            else if (!expressionHasCorrectFormat && userInput == "exit")
            {
                base.ShowResponse(new ShowResponseCommand(this._userInteraction, MessagesTemplates.ByeMessage));
            }

            else if(!expressionHasCorrectFormat)
            {
                base.ShowResponse(new ShowResponseCommand(this._userInteraction, MessagesTemplates.WarnIncorrectInput));
                Run();
            }
        }

        private string GetUserExpression(GetUserInputCommand command)
        {
            command.Execute();

            if (!String.IsNullOrWhiteSpace(command.UserInput))
            {
                return command.UserInput;
            }

            return GetUserExpression(command);
        }
    }
}
