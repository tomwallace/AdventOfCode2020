using AdventOfCode2020.Ten;
using Xunit;

namespace AdventOfCode2020.Tests
{
    public class DayTenTests
    {
        [Theory]
        [InlineData(@"Ten\DayTenTestInputA.txt", 35)]
        [InlineData(@"Ten\DayTenTestInputB.txt", 220)]
        public void FindJoltDifferenceProduct(string filePath, int expected)
        {
            var sut = new DayTen();
            var result = sut.FindJoltDifferenceProduct(filePath);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(@"Ten\DayTenTestInputA.txt", 8)]
        [InlineData(@"Ten\DayTenTestInputB.txt", 19208)]
        public void FindNumberOfValidAdapterCombos(string filePath, int expected)
        {
            var sut = new DayTen();
            var result = sut.FindNumberOfValidAdapterCombos(filePath);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void PartA_Actual()
        {
            var sut = new DayTen();
            var result = sut.PartA();

            Assert.Equal("2210", result);
        }

        // TODO: revisit as is running forever now
        /*
        [Fact]
        public void PartB_Actual()
        {
            var sut = new DayTen();
            var result = sut.PartB();

            Assert.Equal("-1", result);
        }
        */
    }
}