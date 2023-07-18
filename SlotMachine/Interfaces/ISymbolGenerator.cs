using SlotMachine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlotMachine.Interfaces
{
    public interface ISymbolGenerator
    {
        public List<Symbol> GetSymbols(int numberOfSymbols);
    }
}
