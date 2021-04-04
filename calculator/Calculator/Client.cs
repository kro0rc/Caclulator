using System;
using System.Collections.Generic;
using Calculator.UserInteraction;
using Calculator.CalculationProcessor;
using Calculator.Parser;
using Calculator.Commands;

namespace Calculator
{
    public class Client
    {
        private double _result;
        private IUserInteraction _interaction;
        private CalculatorRealization _calculatorMode;
        private ExpressionParser _parserMode;

        public Client(IUserInteraction interaction)
        {
            this._interaction = interaction;

        }

        public void Start()
        {
            GetCalcuratorType(new GetUserCalculatorTypeCommand(this._interaction));

            if (this._calculatorMode != null && this._parserMode != null)
            {
                RunCalculation(this._parserMode, this._calculatorMode);
            }
        }

        private void RunCalculation(ExpressionParser parser, CalculatorRealization calculator)
        {
            string userInput = GetUserExpression(new GetUserInputCommand(this._interaction));

            List<string> parsedExpression = new List<string>();
            parser.CheckAndParse(userInput);

            if (!parser.correctStringFormat)
            {
                ShowResponse(new ShowResponseCommand(this._interaction, "Incorrect expression input! Try again"));
                RunCalculation(parser, calculator);
            }

            this._result = calculator.SimpleCalculating(parser.expressionParts);
            this._interaction.ShowResponse(this._result.ToString());
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

        private void GetCalcuratorType(GetUserCalculatorTypeCommand command)
        {
            command.Execute();

            this._calculatorMode = command.CalculatorType;
            this._parserMode = command.ParserType;
        }

        private void ShowResponse(ICommand command)
        {
            command.Execute();
        }

        //public bool ExitRestartDialog()
        //{

        //}
    }
}
