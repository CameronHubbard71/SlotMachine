using Microsoft.VisualStudio.TestPlatform.Utilities;
using Moq;
using SlotMachine;
using SlotMachine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlotMachineTests
{
    public class GameLogicTests
    {
        private TestUtility _TestUtility;
        private GameLogic _GameLogic;

        [SetUp]
        public void Setup() 
        {
            _TestUtility = new TestUtility();
            _GameLogic = new GameLogic();
        }

        [Test]
        public void DisplayCurrentGrid()
        {
            var grid = new Grid() { Height = 4, Width = 3};
            var symbolList = new List<Symbol>() 
            { 
                new Symbol() {CharacterDisplay = "A"},
                new Symbol() {CharacterDisplay = "B"},
                new Symbol() {CharacterDisplay = "A"},
                new Symbol() {CharacterDisplay = "A"},
                new Symbol() {CharacterDisplay = "B"},
                new Symbol() {CharacterDisplay = "A"},
                new Symbol() {CharacterDisplay = "A"},
                new Symbol() {CharacterDisplay = "B"},
                new Symbol() {CharacterDisplay = "A"},
                new Symbol() {CharacterDisplay = "A"},
                new Symbol() {CharacterDisplay = "B"},
                new Symbol() {CharacterDisplay = "A"}
            };

            _GameLogic.DisplayCurrentGrid(symbolList, grid);

            var outputLines = _TestUtility.GetConsoleOutputStringsWrite();

            Assert.That(outputLines[0], Is.EqualTo("ABA"));
            Assert.That(outputLines[1], Is.EqualTo("ABA"));
            Assert.That(outputLines[2], Is.EqualTo("ABA"));
            Assert.That(outputLines[3], Is.EqualTo("ABA"));
        }

        [Test]
        public void GetWinAmount_WithWin()
        {
            var grid = new Grid() { Height = 4, Width = 3 };
            var symbolList = new List<Symbol>()
            {
                new Symbol() {CharacterDisplay = "A", Coifficent = 0.4M},
                new Symbol() {CharacterDisplay = "A", Coifficent = 0.4M},
                new Symbol() {CharacterDisplay = "A", Coifficent = 0.4M},
                new Symbol() {CharacterDisplay = "A", Coifficent = 0.4M},
                new Symbol() {CharacterDisplay = "B", Coifficent = 0.6M},
                new Symbol() {CharacterDisplay = "A", Coifficent = 0.4M},
                new Symbol() {CharacterDisplay = "A", Coifficent = 0.4M},
                new Symbol() {CharacterDisplay = "B", Coifficent = 0.6M},
                new Symbol() {CharacterDisplay = "A", Coifficent = 0.4M},
                new Symbol() {CharacterDisplay = "A", Coifficent = 0.4M},
                new Symbol() {CharacterDisplay = "B", Coifficent = 0.6M},
                new Symbol() {CharacterDisplay = "A", Coifficent = 0.4M}
            };

            Assert.That(_GameLogic.GetWinAmount(symbolList, 10, grid), Is.EqualTo(12));
        }

        [Test]
        public void GetWinAmount_NoWin()
        {
            var grid = new Grid() { Height = 4, Width = 3 };
            var symbolList = new List<Symbol>()
            {
                new Symbol() {CharacterDisplay = "A", Coifficent = 0.4M},
                new Symbol() {CharacterDisplay = "B", Coifficent = 0.4M},
                new Symbol() {CharacterDisplay = "A", Coifficent = 0.4M},
                new Symbol() {CharacterDisplay = "A", Coifficent = 0.4M},
                new Symbol() {CharacterDisplay = "B", Coifficent = 0.6M},
                new Symbol() {CharacterDisplay = "A", Coifficent = 0.4M},
                new Symbol() {CharacterDisplay = "A", Coifficent = 0.4M},
                new Symbol() {CharacterDisplay = "B", Coifficent = 0.6M},
                new Symbol() {CharacterDisplay = "A", Coifficent = 0.4M},
                new Symbol() {CharacterDisplay = "A", Coifficent = 0.4M},
                new Symbol() {CharacterDisplay = "B", Coifficent = 0.6M},
                new Symbol() {CharacterDisplay = "A", Coifficent = 0.4M}
            };

            Assert.That(_GameLogic.GetWinAmount(symbolList, 10, grid), Is.EqualTo(0));
        }

    }
}
