using AdventOfCode2020.Fifteen;
using System.Collections.Generic;
using Xunit;

namespace AdventOfCode2020.Tests
{
    public class DayFifteenTests
    {
        [Fact]
        public void NumberGame_Play()
        {
            var startingNumbers = new List<int>() { 0, 3, 6 };
            var sut = new NumberGame(startingNumbers);
            var result = sut.Play(2020);

            Assert.Equal(436, result);
        }

        [Fact]
        public void PartA_Actual()
        {
            var sut = new DayFifteen();
            var result = sut.PartA();

            Assert.Equal("639", result);
        }

        [Fact]
        public void PartB_Actual()
        {
            var sut = new DayFifteen();
            var result = sut.PartB();

            Assert.Equal("266", result);
        }
    }
}