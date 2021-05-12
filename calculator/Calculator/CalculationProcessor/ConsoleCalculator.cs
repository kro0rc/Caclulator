using System;
using Calculator.UserInteraction;
using Calculator.Parser;
using Calculator.Commands;
using System.Collections.Generic;

namespace Calculator.CalculationProcessor
{
    public class ConsoleCalculator : CalculatorRealization
    {
        private const string _keyToExit = "exit";

        public ConsoleCalculator(IUserInteraction interaction, ExpressionParser parser)
        {
            this._userInteraction = interaction;
            this._parser = parser;
        }
       

        public override void Run()
        {
            string userInput = GetUserExpression(new UserInputCommand(this._userInteraction));
            
            if (userInput != _keyToExit)
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

            GC.Collect();
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
