using AdventOfCode2020.Nine;
using Xunit;

namespace AdventOfCode2020.Tests
{
    public class DayNineTests
    {
        [Fact]
        public void FindNumberWithWeakness()
        {
            string filePath = @"Nine\DayNineTestInputA.txt";
            var sut = new DayNine();
            var result = sut.FindNumberWithWeakness(filePath, 5);

            Assert.Equal(127, result);
        }

        [Fact]
        public void FindSumOfSmallestAndLargestEqualingTarget()
        {
            string filePath = @"Nine\DayNineTestInputA.txt";
            var sut = new DayNine();
            var result = sut.FindSumOfSmallestAndLargestEqualingTarget(filePath, 127);

            Assert.Equal(62, result);
        }

        [Fact]
        public void PartA_Actual()
        {
            var sut = new DayNine();
            var result = sut.PartA();

            Assert.Equal("14144619", result);
        }

        [Fact]
        public void PartB_Actual()
        {
            var sut = new DayNine();
            var result = sut.PartB();

            Assert.Equal("1766397", result);
        }
    }
}