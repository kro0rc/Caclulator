using System;
using System.Collections.Generic;

namespace Calculator.UserInteraction
{
    public class ConsoleInteraction : IUserInteraction
    {
        private readonly ConsoleKey[] _availableKeys = new ConsoleKey[] { ConsoleKey.C, ConsoleKey.F, ConsoleKey.Escape };
        
        public string GetUserInput(string message)
        {
            ShowResponse(message);
            return Console.ReadLine();
        }

        public ConsoleKey GetUserKey(string message)
        {
            ShowResponse(message);

            var userInput = Console.ReadKey();
            Console.Clear();

            for (int i = 0; i < this._availableKeys.Length; i++)
            {
                if(userInput.Key == this._availableKeys[i])
                {
                    return userInput.Key;
                }
            }

            return GetUserKey(message);
        }

        public void ShowResponse(string message)
        {
            Console.WriteLine(message);
        }

        public void CleanOutput()
        {
            Console.Clear();
        }
       
    }
}
