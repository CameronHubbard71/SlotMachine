using Moq;
using SlotMachine;
using System.Text;

namespace SlotMachineTests
{
    public class AccountManagerTests
    {
        private TestUtility _TestUtility;
        private AccountManager _AccountManager;
        
        [SetUp]
        public void Setup()
        {

            _TestUtility = new TestUtility();
            _AccountManager = new AccountManager(_TestUtility.MockConfig, new List<string>());
        }

        [Test]
        public void GetUserBalance_ValidInput()
        {
            _TestUtility.SetupUserResponses("200");
            _AccountManager.GetUserBalance();

            Assert.That(_AccountManager.Balance, Is.EqualTo(200));
        }

        [Test]
        public void GetUserBalance_InvalidInput()
        {
            _TestUtility.SetupUserResponses("ABCD","200");
            _AccountManager.GetUserBalance();

            var outputLines = _TestUtility.GetConsoleOutputStringsWriteLine();

            Assert.That(outputLines[1], Is.EqualTo("Invalid input, balance must be a valid number over 0.01"));
        }

        [Test]
        public void GetUserStake_ValidInput()
        {
            _AccountManager.Balance = 200;
            _TestUtility.SetupUserResponses("20");
            _AccountManager.GetUserStake();

            Assert.That(_AccountManager.CurrentStake, Is.EqualTo(20));
        }

        [Test]
        public void GetUserStake_InvalidInput_HigherThanBalance()
        {
            _AccountManager.Balance = 200;
            _TestUtility.SetupUserResponses("201","20");
            _AccountManager.GetUserStake();
            var outputLines = _TestUtility.GetConsoleOutputStringsWriteLine();

            Assert.That(outputLines[1], Is.EqualTo("Invalid input, stake must be a valid amount over 0.01 that is less than or equal to the balance:"));
        }

        [Test]
        public void GetUserStake_InvalidInput_LetterValueEntered()
        {
            _AccountManager.Balance = 200;
            _TestUtility.SetupUserResponses("ABCD", "20");
            _AccountManager.GetUserStake();
            var outputLines = _TestUtility.GetConsoleOutputStringsWriteLine();

            Assert.That(outputLines[1], Is.EqualTo("Invalid input, stake must be a valid amount over 0.01 that is less than or equal to the balance:"));
        }

        [Test]
        public void DisplayCurrentBalance()
        {
            _AccountManager.Balance = 200.1M;
            _AccountManager.DisplayCurrentBalance();
            var outputLines = _TestUtility.GetConsoleOutputStringsWriteLine();

            Assert.That(outputLines[0], Is.EqualTo("Current balance is: 200.10"));
        }

        [Test]
        public void SubmitRoundWinToBalance()
        {
            _AccountManager.Balance = 200;
            _AccountManager.CurrentStake = 20;
            _AccountManager.SubmitRoundWinToBalance(30);
            var outputLines = _TestUtility.GetConsoleOutputStringsWriteLine();

            Assert.That(_AccountManager.Balance, Is.EqualTo(210));
        }

        [Test]
        public void CheckEmptyBalance_NotEmpty()
        {
            _AccountManager.Balance = 200;

            Assert.False(_AccountManager.CheckEmptyBalance());
        }

        [Test]
        public void CheckEmptyBalance_IsEmpty()
        {
            _AccountManager.Balance = 0;

            Assert.True(_AccountManager.CheckEmptyBalance());
        }
    }
}