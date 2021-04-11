using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator.UserInteraction
{
    public sealed class MessagesTemplates
    {
        public const string MainMenu = "1. Console calcualtor\n2. File calculator\n3. Exit";
        public const string WelocmeMessage = "Hello! This is calculator.";
        public const string RealizationChoose = "Press Esc to exit\nPress C to procced with console or F to upload file : ";
        public const string GetExpression = "Type \"exit\" to go to main menu \n\nEnter expression to calculate : ";
        public const string GetPath = "Enter path to file : ";
        public const string WarnEmptyInput = "Input is empty!";
        public const string WarnIncorrectInput = "Incorrect Input!";
        public const string ExitOrRestart = "Press R for restart or Esc to exit...";
        public const string ByeMessage = "Bye!";
    }
}
