﻿using System;
using Calculator.UserInteraction;
using Calculator.CalculationProcessor;
using Calculator.Parser;

namespace Calculator.Commands
{
    public class GetUserCalculatorTypeCommand : ICommand, IGetTypeCommand
    {
        public CalculatorRealization CalculatorType { get; private set; }
        private string _pathToFile;
        private IUserInteraction _interaction;
        private ConsoleKey _userKey;
        private ConsoleKey _keyToConsoleCalculator = ConsoleKey.C;
        private ConsoleKey _keyToFileCalculator = ConsoleKey.F;
        private ConsoleKey _keyToCancelOperation = ConsoleKey.Escape;


        public GetUserCalculatorTypeCommand(IUserInteraction interaction, string pathToFile)
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
                return new FileCalculator(pathToFile);
            }

            if (this._userKey == this._keyToConsoleCalculator)
            {
                return new ConsoleCalculator();
            }
            else if (this._userKey == this._keyToFileCalculator)
            {
                return new FileCalculator(pathToFile);
            }

            return null;
        }
    }
}
