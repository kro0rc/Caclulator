using System;
using Calculator.UserInteraction;
using Calculator.CalculationProcessor;
using Calculator.Parser;

namespace Calculator.Commands
{
    public class CalculatorTypeCommand : ICommand, ICalculatorTypeCommand
    {
        public CalculatorRealization CalculatorType { get; private set; }
        private readonly string _pathToFile;
        private readonly IUserInteraction _interaction;
        private ConsoleKey _userKey;
        private readonly ConsoleKey _keyToConsoleCalculator = ConsoleKey.C;
        private readonly ConsoleKey _keyToFileCalculator = ConsoleKey.F;

        public CalculatorTypeCommand(IUserInteraction interaction, string pathToFile)
        {
            this._interaction = interaction;
            this._pathToFile = pathToFile;
        }

        public void Execute()
        {
            this.CalculatorType = GetCalcualtorType(this._pathToFile);
        }

        private CalculatorRealization GetCalcualtorType(string pathToFile)
        {
            this._interaction.CleanOutput();
            this._interaction.ShowResponse(MessagesTemplates.WelocmeMessage);
            this._userKey = this._interaction.GetUserKey(MessagesTemplates.RealizationChoose);

            if (!String.IsNullOrWhiteSpace(pathToFile))
            {
                return new FileCalculator(new ConsoleInteraction(), new FileExpressionParser(), pathToFile);
            }

            if (this._userKey == this._keyToConsoleCalculator)
            {
                return new ConsoleCalculator(this._interaction, new ConsoleParser());
            }
            else if (this._userKey == this._keyToFileCalculator)
            {
                return new FileCalculator(new ConsoleInteraction(), new FileExpressionParser(), pathToFile);
            }

            return null;
        }
    }
}
