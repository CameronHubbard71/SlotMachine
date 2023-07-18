using Microsoft.Extensions.Configuration;
using SlotMachine.Interfaces;
using SlotMachine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SlotMachine
{
    public class SymbolGenerator : ISymbolGenerator
    {
        private readonly List<Symbol>? SymbolsToPick;
        public SymbolGenerator(IConfiguration config, List<String> Errors)
        {
            try
            {
                SymbolsToPick = config.GetRequiredSection("AvailableSymbols").Get<List<Symbol>>();
            }
            catch (Exception ex)
            {
                Errors.Add(ex.Message + " Make sure this is set in the appsettings file before launch");
            }
        }

        public List<Symbol> GetSymbols(int numberOfSymbols)
        {
            var choosenSymbols = new List<Symbol>();
            for(var i = 0; i < numberOfSymbols; i++)
            {
                choosenSymbols.Add(GetRandomSymbol());
            }
            return choosenSymbols;
        }

        private Symbol GetRandomSymbol()
        {
            var random = new Random();
            decimal randomNum = new decimal(random.NextDouble());

            decimal cumulative = 0;
            foreach (var symbol in SymbolsToPick)
            {
                cumulative += symbol.Probability;
                if (randomNum < cumulative)
                {
                    return symbol;
                }
            }

            return null;
        }
    }
}
