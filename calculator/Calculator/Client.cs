using Calculator.UserInteraction;
using Calculator.CalculationProcessor;
using Calculator.Commands;

namespace Calculator
{
    public class Client
    {
        private string _pathToFile;
        public IUserInteraction _interaction { get; private set; }
        public CalculatorRealization _calculatorMode { get; private set; }
        
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
