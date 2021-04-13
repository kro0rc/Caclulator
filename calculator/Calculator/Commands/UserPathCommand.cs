using System;
using Calculator.UserInteraction;

namespace Calculator.Commands
{
    class UserPathCommand : ICommand, IDataComand
    {
        public string UserInput { get; private set; }
        private IUserInteraction _interaction;

        public UserPathCommand(IUserInteraction interaction)
        {
            this._interaction = interaction;
        }

        public void Execute()
        {
            this.UserInput = this._interaction.GetUserInput(MessagesTemplates.GetPath);
        }
    }
}
