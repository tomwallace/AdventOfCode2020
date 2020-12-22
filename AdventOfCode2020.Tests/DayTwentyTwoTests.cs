using AdventOfCode2020.TwentyTwo;
using Xunit;

namespace AdventOfCode2020.Tests
{
    public class DayTwentyTwoTests
    {
        [Fact]
        public void PlayGameAndReturnWinningScore()
        {
            string filePath = @"TwentyTwo\DayTwentyTwoTestInputA.txt";
            var sut = new DayTwentyTwo();
            var result = sut.PlayGameAndReturnWinningScore(filePath, false);

            Assert.Equal(306, result);
        }

        [Fact]
        public void PlayGameAndReturnWinningScore_Recursive()
        {
            string filePath = @"TwentyTwo\DayTwentyTwoTestInputA.txt";
            var sut = new DayTwentyTwo();
            var result = sut.PlayGameAndReturnWinningScore(filePath, true);

            Assert.Equal(291, result);
        }

        [Fact]
        public void PartA_Actual()
        {
            var sut = new DayTwentyTwo();
            var result = sut.PartA();

            Assert.Equal("35397", result);
        }

        // 7518 too low
        [Fact]
        public void PartB_Actual()
        {
            var sut = new DayTwentyTwo();
            var result = sut.PartB();

            Assert.Equal("31120", result);
        }
    }
}