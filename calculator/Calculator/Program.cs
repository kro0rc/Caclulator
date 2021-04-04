using Calculator.UserInteraction;
using System.Linq;

namespace Calculator
{  
    class Program
    {
        private Client _client;

        static void Main(string[] args)
        {
            Program program = new Program();
            program.StartProgram(args?.FirstOrDefault());
        }

        public void StartProgram(string predefinedPath)
        {
            this._client = new Client(new ConsoleInteraction(), predefinedPath);
            this._client.Init();
        }
    }
}
