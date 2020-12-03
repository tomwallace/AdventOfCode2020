using AdventOfCode2020.Three;
using Xunit;

namespace AdventOfCode2020.Tests
{
    public class DayThreeTests
    {
        [Fact]
        public void CalculateTreesHit()
        {
            string filePath = @"Three\DayThreeTestInputA.txt";
            var slope = new Slope(1, 3);

            var sut = new DayThree();
            var result = sut.CalculateTreesHit(filePath, slope);

            Assert.Equal(7, result);
        }

        [Fact]
        public void ProductOfMultipleRuns()
        {
            string filePath = @"Three\DayThreeTestInputA.txt";

            var sut = new DayThree();
            var result = sut.ProductOfMultipleRuns(filePath);

            Assert.Equal(336, result);
        }

        [Fact]
        public void PartA_Actual()
        {
            var sut = new DayThree();
            var result = sut.PartA();

            Assert.Equal("220", result);
        }

        [Fact]
        public void PartB_Actual()
        {
            var sut = new DayThree();
            var result = sut.PartB();

            Assert.Equal("2138320800", result);
        }
    }
}