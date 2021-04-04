using System;
using Calculator.UserInteraction;

namespace Calculator.Commands
{
    public class GetUserInputCommand : ICommand, IGetDataComand
    {
        public string UserInput { get; private set; }
        private IUserInteraction _interaction;

        public GetUserInputCommand(IUserInteraction interaction)
        {
            this._interaction = interaction;
        }

        public void Execute()
        {
            this.UserInput = this._interaction.GetUserInput(MessagesTemplates.GetExpression);
        }

        public void Execute(string message)
        {

        }
    }
}
