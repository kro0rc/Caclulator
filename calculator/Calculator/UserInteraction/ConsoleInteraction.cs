using System;
using System.Collections.Generic;

namespace Calculator.UserInteraction
{
    public class ConsoleInteraction : IUserInteraction
    {
        
        public string GetUserInput(string message)
        {
            ShowResponse(message);
            return Console.ReadLine();
        }

        public ConsoleKey GetUserKey(string message)
        {
            List<ConsoleKey> availableKeys = new List<ConsoleKey>() { ConsoleKey.C, ConsoleKey.F, ConsoleKey.Escape };
            ShowResponse(message);

            var userInput = Console.ReadKey();
            Console.Clear();

            if(availableKeys.Contains(userInput.Key))
            {
                return userInput.Key;
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
