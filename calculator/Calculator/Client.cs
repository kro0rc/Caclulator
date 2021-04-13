using Calculator.UserInteraction;
using Calculator.CalculationProcessor;
using Calculator.Commands;

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
            SetCalcuratorType(new CalculatorTypeCommand(this._interaction, this._pathToFile));
            this._calculatorMode?.Run();
        }

        private void SetCalcuratorType(CalculatorTypeCommand command)
        {
            command.Execute();
            this._calculatorMode = command.CalculatorType;
        }
    }
}
