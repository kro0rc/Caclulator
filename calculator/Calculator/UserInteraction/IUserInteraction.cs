using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator.UserInteraction
{
    public interface IUserInteraction
    {
        string GetUserInput(string message);

        ConsoleKey GetUserKey(string message);
        void ShowResponse(string message);
        void CleanOutput();
    }
}
