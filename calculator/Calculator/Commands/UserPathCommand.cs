using System;
using Calculator.UserInteraction;

namespace Calculator.Commands
{
    public class UserPathCommand : ICommand, IDataComand
    {
        public string UserInput { get; private set; }
        private readonly IUserInteraction _interaction;

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
