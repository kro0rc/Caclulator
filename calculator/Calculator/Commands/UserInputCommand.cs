using System;
using Calculator.UserInteraction;

namespace Calculator.Commands
{
    public class UserInputCommand : ICommand, IDataComand
    {
        public string UserInput { get; private set; }
        private IUserInteraction _interaction;

        public UserInputCommand(IUserInteraction interaction)
        {
            this._interaction = interaction;
        }

        public void Execute()
        {
            this.UserInput = this._interaction.GetUserInput(MessagesTemplates.GetExpression);
        }
    }
}
