using AdventOfCode2020.Nineteen;
using Xunit;

namespace AdventOfCode2020.Tests
{
    public class DayNineteenTests
    {
        [Fact]
        public void PartA_Actual()
        {
            var sut = new DayNineteen();
            var result = sut.PartA();

            Assert.Equal("265", result);
        }

        [Fact]
        public void PartB_Actual()
        {
            var sut = new DayNineteen();
            var result = sut.PartB();

            Assert.Equal("394", result);
        }
    }
}