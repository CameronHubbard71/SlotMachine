using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlotMachine.Models
{
    public class Symbol
    {
        public string CharacterDisplay { get; set; }
        public decimal Coifficent { get; set; }
        public decimal Probability { get; set; }
        public bool HasWildCardRule { get; set; }
    }
}
