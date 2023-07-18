using SlotMachine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlotMachine.Interfaces
{
    public interface IGameLogic
    {
        public void DisplayCurrentGrid(List<Symbol> symbols, Grid grid);
        public decimal GetWinAmount(List<Symbol> choosenSymbols, decimal stake, Grid grid);
    }
}
