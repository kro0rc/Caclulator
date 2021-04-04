using Calculator.UserInteraction;

namespace Calculator
{  
    class Program
    {
        private Client _client;

        static void Main(string[] args)
        {
            Program program = new Program();
            program.Init();
        }

        public void Init()
        {
            this._client = new Client(new ConsoleInteraction());
            this._client.Start();
        }
    }
}
