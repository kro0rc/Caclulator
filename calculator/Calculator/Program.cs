using Calculator.UserInteraction;
using System.Linq;

namespace Calculator
{  
    class Program
    {
        static void Main(string[] args)
        {
            Client client = new Client(new ConsoleInteraction(), args?.FirstOrDefault());
            client.Init();
        }
    }
}
