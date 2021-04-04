using System;
using Calculator.UserInteraction;

namespace Calculator.Commands
{
    class ShowResponseCommand : ICommand
    {
        private IUserInteraction _interaction;
        private readonly string _message;

        public ShowResponseCommand(IUserInteraction interaction, string message)
        {
            this._interaction = interaction;
            this._message = message;
        }
        public void Execute()
        {
            this._interaction.ShowResponse(this._message);
        }
    }
}
