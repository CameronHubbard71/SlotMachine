using Castle.Core.Configuration;
using Microsoft.Extensions.Configuration;
using SlotMachine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlotMachineTests
{
    public class SymbolGeneratorTests
    {
        private SymbolGenerator _SymbolGenerator;
        private TestUtility _TestUtility;

        [SetUp]
        public void Setup()
        {

            _TestUtility = new TestUtility();
            _SymbolGenerator = new SymbolGenerator(_TestUtility.MockConfig, new List<String>());
        }

        [Test]
        public void GetSelectedSymbols()
        {
            var symbols = _SymbolGenerator.GetSymbols(10);
            Assert.That(symbols.Count, Is.EqualTo(10));
        }
    }
}
