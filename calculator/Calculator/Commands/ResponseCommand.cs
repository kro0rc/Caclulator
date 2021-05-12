using System;
using Calculator.UserInteraction;

namespace Calculator.Commands
{
    public class ResponseCommand : ICommand
    {
        private IUserInteraction _interaction;
        private readonly string _message;

        public ResponseCommand(IUserInteraction interaction, string message)
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
