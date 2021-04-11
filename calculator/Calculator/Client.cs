using System;
using System.Collections.Generic;
using Calculator.UserInteraction;
using Calculator.CalculationProcessor;
using Calculator.Parser;
using Calculator.Commands;
using Calculator.FileProcessor;
using System.Globalization;

namespace Calculator
{
    public class Client
    {
        private string _pathToFile;
        private IUserInteraction _interaction;
        private CalculatorRealization _calculatorMode;
        
        public Client(IUserInteraction interaction, string pathToFile)
        {
            this._interaction = interaction;
            this._pathToFile = pathToFile;
        }

        public void Init()
        {
            SetCalcuratorType(new GetUserCalculatorTypeCommand(this._interaction, this._pathToFile));
            
            if(this._calculatorMode != null)
            {
                this._calculatorMode.Run();
            }
        }

        private void SetCalcuratorType(GetUserCalculatorTypeCommand command)
        {
            command.Execute();
            this._calculatorMode = command.CalculatorType;
        }
    }
}
