using System;
using System.Collections.Generic;
using System.Text;

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
            ShowResponse(message);

            var input = Console.ReadKey();

            switch (input.Key)
            {
                case ConsoleKey.C:
                    Console.Clear();
                    return ConsoleKey.C;

                case ConsoleKey.F:
                    Console.Clear();
                    return ConsoleKey.F;

                case ConsoleKey.R:
                    Console.Clear();
                    return ConsoleKey.R;

                case ConsoleKey.Escape:
                    Console.Clear();
                    return ConsoleKey.Escape;

                default:
                    Console.Clear();
                    ShowResponse(MessagesTemplates.WarnIncorrectInput);
                    break;
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
