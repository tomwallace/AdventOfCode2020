using AdventOfCode2020.Five;
using Xunit;

namespace AdventOfCode2020.Tests
{
    public class DayFiveTests
    {
        [Theory]
        [InlineData("FBFBBFFRLR", 357)]
        [InlineData("BFFFBBFRRR", 567)]
        [InlineData("FFFBBBFRRR", 119)]
        [InlineData("BBFFBBFRLL", 820)]
        public void CalculateSeatId(string seatLocator, int expected)
        {
            var sut = new AirplaneSeat(seatLocator);
            var result = sut.GetSeatId();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void PartA_Actual()
        {
            var sut = new DayFive();
            var result = sut.PartA();

            Assert.Equal("866", result);
        }

        [Fact]
        public void PartB_Actual()
        {
            var sut = new DayFive();
            var result = sut.PartB();

            Assert.Equal("583", result);
        }
    }
}