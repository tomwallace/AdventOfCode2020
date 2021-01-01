using AdventOfCode2020.Nineteen;
using Xunit;

namespace AdventOfCode2020.Tests
{
    public class DayNineteenTests
    {

        [Fact]
        public void CountValidMessagesRecursive()
        {
            string rulesFilePath = @"Nineteen\DayNineteenTestInputA_Rules.txt";
            string messagesFilePath = @"Nineteen\DayNineteenTestInputA_Messages.txt";

            var sut = new DayNineteen();
            var result = sut.CountValidMessagesRecursive(rulesFilePath, messagesFilePath);

            Assert.Equal(2, result);
        }

        /*
        [Fact]
        public void GenerateCompoundValidMessage()
        {
            string rulesFilePath = @"Nineteen\DayNineteenTestInputA_Rules.txt";
            var sut = new DayNineteen();
            var result = sut.GenerateCompoundValidMessage(rulesFilePath);
            
            // TODO: This was just to capture output and may not result in correct values
            var expected = "a ((a a | b b) (a b | b a) | (a b | b a) (a a | b b)) b";

            Assert.Equal(expected, result);

        }

        [Fact]
        public void PossibleValidMessages()
        {
            string rulesFilePath = @"Nineteen\DayNineteenTestInputA_Rules.txt";
            var sut = new DayNineteen();
            var result = sut.PossibleValidMessages(rulesFilePath);

            // TODO: Just did this to capture output, not sure it is right
            Assert.Equal(13, result.Count);
        }
        */

        /*
        [Fact]
        public void PartA_Actual()
        {
            var sut = new DayNineteen();
            var result = sut.PartA();

            Assert.Equal("-1", result);
        }
        */

        [Fact]
        public void PartB_Actual()
        {
            var sut = new DayNineteen();
            var result = sut.PartB();

            Assert.Equal("-1", result);
        }
    }
}