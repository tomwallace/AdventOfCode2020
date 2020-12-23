using AdventOfCode2020.TwentyThree;
using Xunit;

namespace AdventOfCode2020.Tests
{
    public class DayTwentyThreeTests
    {
        [Theory]
        [InlineData(10, "92658374")]
        [InlineData(100, "67384529")]
        public void CupGame_Play(int moves, string expected)
        {
            var input = "389125467";
            var sut = new CupGame(input);
            sut.Play(moves);
            var result = sut.ListCupOrder();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void CupGame_BigPlay()
        {
            var input = "389125467";
            var sut = new CupGame(input, 1000000);
            sut.Play(10000000);
            
            var right = sut.GetCup(1);
            Assert.Equal(934001, right.Id);

            var doubleRight = right.Right;
            Assert.Equal(159792, doubleRight.Id);
        }

        [Fact]
        public void PartA_Actual()
        {
            var sut = new DayTwentyThree();
            var result = sut.PartA();

            Assert.Equal("47598263", result);
        }

        [Fact]
        public void PartB_Actual()
        {
            var sut = new DayTwentyThree();
            var result = sut.PartB();

            Assert.Equal("-1", result);
        }
    }
}