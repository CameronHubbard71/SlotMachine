using SlotMachine.Interfaces;
using SlotMachine.Models;

namespace SlotMachine
{
    public class GameLogic : IGameLogic
    {
        public void DisplayCurrentGrid(List<Symbol> symbols, Grid grid)
        {
            var currentColumnCount = 0;
            foreach (var symbol in symbols)
            {
                Console.Write(symbol.CharacterDisplay);

                currentColumnCount++;
                if (currentColumnCount == grid.Width)
                {
                    currentColumnCount = 0;
                    Console.Write("\n");
                }
            }
            Console.Write("\n");
        }

        public decimal GetWinAmount(List<Symbol> choosenSymbols, decimal stake, Grid grid)
        {
            decimal winTotal = 0;
            var currentRowSymbols = new List<Symbol>();
            foreach(var symbol in choosenSymbols)
            {
                currentRowSymbols.Add(symbol);
                if (currentRowSymbols.Count == grid.Width)
                {
                    if (CheckMatch(currentRowSymbols))
                    {
                        decimal totalCoifficent = currentRowSymbols.Select(s => s.Coifficent).Sum();
                        winTotal += (totalCoifficent * stake);
                    }
                    currentRowSymbols.Clear();
                }
            }
            return winTotal;
        }

        private static bool CheckMatch(List<Symbol> symbols)
        {
            var symbolToMatch = symbols.First().CharacterDisplay;
            return symbols.Where(s => s.CharacterDisplay == symbolToMatch || s.HasWildCardRule).ToList().Count == symbols.Count;
        }

    }
}
