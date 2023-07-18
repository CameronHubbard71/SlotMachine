using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlotMachineTests
{
    public class TestUtility
    {
        private readonly StringBuilder _ConsoleOutput;
        private readonly Mock<TextReader> _ConsoleInput;
        public IConfiguration MockConfig;

        public TestUtility()
        {
            _ConsoleOutput = new StringBuilder();
            var consoleOutputWriter = new StringWriter(_ConsoleOutput);
            _ConsoleInput = new Mock<TextReader>();
            Console.SetOut(consoleOutputWriter);
            Console.SetIn(_ConsoleInput.Object);

            var appSettings =
            @"{""AvailableSymbols"": [
              {
                ""CharacterDisplay"": ""A"",
                ""Coifficent"": ""0.4"",
                ""Probability"": ""0.45""
              },
              {
                ""CharacterDisplay"": ""B"",
                ""Coifficent"": ""0.6"",
                ""Probability"": ""0.35""
              },
              {
                ""CharacterDisplay"": ""P"",
                ""Coifficent"": ""0.8"",
                ""Probability"": ""0.15""
              },
              {
                ""CharacterDisplay"": ""*"",
                ""Coifficent"": ""0.0"",
                ""Probability"": ""0.05"",
                ""HasWildCardRule"": true
              }
            ], ""MinimumStake"": ""0.01"",

            ""MinimumBalance"": ""0.01""}";

            var builder = new ConfigurationBuilder();
            builder.AddJsonStream(new MemoryStream(Encoding.UTF8.GetBytes(appSettings)));
            MockConfig = builder.Build();
        }

        public string[]? GetConsoleOutputStringsWriteLine()
        {
            return _ConsoleOutput.ToString().Split("\r\n");
        }

        public string[]? GetConsoleOutputStringsWrite()
        {
            return _ConsoleOutput.ToString().Split("\n");
        }

        public MockSequence SetupUserResponses(params string[] responses)
        {
            var sequence = new MockSequence();
            foreach (var response in responses)
            {
                _ConsoleInput.InSequence(sequence).Setup(x => x.ReadLine()).Returns(response);
            }
            return sequence;
        }
    }
}
