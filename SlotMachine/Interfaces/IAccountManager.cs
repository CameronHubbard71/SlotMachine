using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlotMachine.Interfaces
{
    public interface IAccountManager
    {
        public decimal Balance { get; set; }
        public decimal CurrentStake { get; set; }
        public void GetUserBalance();
        public void GetUserStake();
        public void DisplayCurrentBalance();
        public decimal SubmitRoundWinToBalance(decimal totalWin);
        public bool CheckEmptyBalance();
    }
}
