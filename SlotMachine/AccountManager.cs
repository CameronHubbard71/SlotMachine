using Microsoft.Extensions.Configuration;
using SlotMachine.Interfaces;
using SlotMachine.Models;

namespace SlotMachine
{
    public class AccountManager : IAccountManager
    {
        public decimal Balance { get; set; }
        public decimal CurrentStake { get; set; }
        private readonly decimal MinimumStake;
        private readonly decimal MinimumBalance;

        public AccountManager(IConfiguration config, List<string> Errors)
        {
            try
            {
                MinimumStake = config.GetRequiredSection("MinimumStake").Get<decimal>();
                MinimumBalance = config.GetRequiredSection("MinimumBalance").Get<decimal>();
            }
            catch (Exception ex)
            {
                Errors.Add(ex.Message + " Make sure this is set in the appsettings file before launch");
            }
        }


        public void GetUserBalance()
        {
            Console.WriteLine("Please deposit money you would like to play with:");
            var userInput = Console.ReadLine();
            var validInput = false;
            while (!validInput)
            {
                if (decimal.TryParse(userInput, out decimal balance) && balance >= MinimumBalance)
                {
                    Balance = balance;
                    validInput = true;
                }
                else
                {
                    Console.WriteLine($"Invalid input, balance must be a valid number over {MinimumBalance}");
                    userInput = Console.ReadLine();
                }
                
            }
        }

        public void GetUserStake()
        {
            Console.WriteLine("Enter stake amount");

            var userInput = Console.ReadLine();

            var validInput = false;
            while(!validInput)
            {
                if(decimal.TryParse(userInput, out decimal stake) && stake <= Balance && stake >= MinimumStake)
                {
                    CurrentStake = stake;
                    validInput = true;                   
                }
                else
                {
                    Console.WriteLine($"Invalid input, stake must be a valid amount over {MinimumStake} that is less than or equal to the balance:");
                    userInput = Console.ReadLine();
                }
            }
            Console.Write("\n");
        }

        public void DisplayCurrentBalance()
        {
            Console.WriteLine("Current balance is: " + Balance.ToString("0.00"));
        }

        public decimal SubmitRoundWinToBalance(decimal totalWin)
        {
            Console.WriteLine("You have won: " + totalWin.ToString("0.00"));
            
            Balance += totalWin - CurrentStake;
            Balance = Math.Round(Balance, 2);
            return Balance; 
        }

        public bool CheckEmptyBalance()
        {
            return Balance <= 0;
        }
        



    }
}
