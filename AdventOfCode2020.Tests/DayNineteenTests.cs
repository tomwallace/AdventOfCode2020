using AdventOfCode2020.Nineteen;
using Xunit;

namespace AdventOfCode2020.Tests
{
    public class DayNineteenTests
    {

        [Fact]
        public void CountValidMessages()
        {
            string rulesFilePath = @"Nineteen\DayNineteenTestInputA_Rules.txt";
            string messagesFilePath = @"Nineteen\DayNineteenTestInputA_Messages.txt";

            var sut = new DayNineteen();
            var result = sut.CountValidMessages(rulesFilePath, messagesFilePath);

            Assert.Equal(2, result);
        }

        // TODO runs forever
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