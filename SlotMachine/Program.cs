using Microsoft.Extensions.Configuration;
using SlotMachine.Interfaces;

namespace SlotMachine
{
    public class Program
    {
        public static void Main()
        {
            IConfiguration config = new ConfigurationBuilder()
            .AddEnvironmentVariables()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

            IMainGame mainclass = new MainGame(config);
            mainclass.StartGame();
        }

    }
}