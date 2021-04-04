using System;
using Calculator.UserInteraction;
using Calculator.CalculationProcessor;
using Calculator.Parser;

namespace Calculator.Commands
{
    public class GetUserCalculatorTypeCommand : ICommand, IGetTypeCommand
    {
        public CalculatorRealization CalculatorType { get; private set; }
        public ExpressionParser ParserType { get; private set; }
        private IUserInteraction _interaction;
        private ConsoleKey _userKey;
        private ConsoleKey _keyToConsoleCalculator = ConsoleKey.C;
        private ConsoleKey _keyToFileCalculator = ConsoleKey.F;
        private ConsoleKey _keyToCancelOperation = ConsoleKey.Escape;


        public GetUserCalculatorTypeCommand(IUserInteraction interaction)
        {
            this._interaction = interaction;
        }


        public void Execute()
        {
            this._interaction.CleanOutput();
            this._interaction.ShowResponse(MessagesTemplates.WelocmeMessage);
            this._userKey = this._interaction.GetUserKey(MessagesTemplates.RealizationChoose);

            if (this._userKey == this._keyToConsoleCalculator)
            {
                this.CalculatorType = new ConsoleCalculator();
                this.ParserType = new ConsoleParser();
            }
            else if (this._userKey == this._keyToFileCalculator)
            {
                this.CalculatorType = new FileCalculator();
                this.ParserType = new FileExpressionParser();
            }
            else if (this._userKey == this._keyToCancelOperation)
            {
                
            }

        }
    }
}
