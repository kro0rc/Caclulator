using System;
using Calculator.UserInteraction;
using Calculator.Parser;
using Calculator.Commands;
using System.Collections.Generic;

namespace Calculator.CalculationProcessor
{
    class ConsoleCalculator : CalculatorRealization
    {
        private string _keyToExit = "exit";
        private IUserInteraction _userInteraction;
        private ExpressionParser _parser;

        public ConsoleCalculator()
        {
            this._userInteraction = new ConsoleInteraction();
            this._parser = new ConsoleParser();
        }

        public override void Run()
        {
            string userInput = GetUserExpression(new UserInputCommand(this._userInteraction));
            
            if (userInput != this._keyToExit)
            {                
                if (this._parser.CheckExpressionBeforeParsing(userInput))
                {
                    List<string> parsedExpression = this._parser.ParseExpression(userInput);

                    if (CheckParsedExpression(parsedExpression))
                    {
                        double result = base.SimpleCalculating(parsedExpression);
                        base.ShowResponse(new ResponseCommand(this._userInteraction, result.ToString()));
                        Run();
                    }

                    base.ShowResponse(new ResponseCommand(this._userInteraction, MessagesTemplates.WarnIncorrectInput));
                }

                base.ShowResponse(new ResponseCommand(this._userInteraction, MessagesTemplates.WarnIncorrectInput));
                Run();
            }
        }

        private string GetUserExpression(UserInputCommand command)
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
