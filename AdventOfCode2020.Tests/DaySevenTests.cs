using AdventOfCode2020.Seven;
using Xunit;

namespace AdventOfCode2020.Tests
{
    public class DaySevenTests
    {
        [Fact]
        public void GetNumberOfBagsThatContainShinyGold()
        {
            string filePath = @"Seven\DaySevenTestInputA.txt";
            var sut = new DaySeven();
            var result = sut.GetNumberOfBagsThatContainShinyGold(filePath);

            Assert.Equal(4, result);
        }

        [Fact]
        public void GetTotalNumberOfBagsContained()
        {
            string filePath = @"Seven\DaySevenTestInputA.txt";
            var sut = new DaySeven();
            var result = sut.GetTotalNumberOfBagsContainedByShinyGold(filePath);

            Assert.Equal(32, result);
        }

        [Fact]
        public void PartA_Actual()
        {
            var sut = new DaySeven();
            var result = sut.PartA();

            Assert.Equal("355", result);
        }

        [Fact]
        public void PartB_Actual()
        {
            var sut = new DaySeven();
            var result = sut.PartB();

            Assert.Equal("5312", result);
        }
    }
}