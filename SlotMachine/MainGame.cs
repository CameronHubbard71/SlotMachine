using Microsoft.Extensions.Configuration;
using SlotMachine.Interfaces;
using SlotMachine.Models;

namespace SlotMachine
{
    public class MainGame : IMainGame
    {
        private readonly IConfiguration _config;
        private readonly Grid? Grid;
        private readonly List<string> Errors = new();   
        public MainGame(IConfiguration config) {
            _config = config;

            try
            {
                Grid = config.GetRequiredSection("GridSize").Get<Grid>();
            }
            catch (Exception ex)
            {
                Errors.Add(ex.Message + " Make sure this is set in the appsettings file before launch");
            }
            
        }

        public void StartGame()
        {
            IGameLogic gameLogicManager = new GameLogic();
            ISymbolGenerator symbolGenerator = new SymbolGenerator(_config,Errors);
            IAccountManager accountManager = new AccountManager(_config, Errors);

            if(Errors.Any())
            {
                Console.WriteLine("Errors Found During Setup: ");
                foreach(var error in Errors)
                {
                    Console.WriteLine(error);
                }
                return;
            }

            accountManager.GetUserBalance();

            while(!accountManager.CheckEmptyBalance())
            {
                accountManager.GetUserStake();

                var choosenSymbols = symbolGenerator.GetSymbols(Grid.Height * Grid.Width);
                gameLogicManager.DisplayCurrentGrid(choosenSymbols,Grid);
                
                var winAmount = gameLogicManager.GetWinAmount(choosenSymbols,accountManager.CurrentStake,Grid);

                accountManager.SubmitRoundWinToBalance(winAmount);
                
                accountManager.DisplayCurrentBalance();
            }
        }

        


    }
}
