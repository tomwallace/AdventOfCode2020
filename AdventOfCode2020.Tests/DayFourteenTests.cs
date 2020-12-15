using AdventOfCode2020.Fourteen;
using Xunit;

namespace AdventOfCode2020.Tests
{
    public class DayFourteenTests
    {
        [Fact]
        public void RunProgramAndReturnSum()
        {
            string filePath = @"Fourteen\DayFourteenTestInputA.txt";
            var sut = new DayFourteen();
            var result = sut.RunProgramAndReturnSum(filePath, false);

            Assert.Equal(165, result);
        }

        [Fact]
        public void RunProgramAndReturnSum_PartTwo()
        {
            string filePath = @"Fourteen\DayFourteenTestInputB.txt";
            var sut = new DayFourteen();
            var result = sut.RunProgramAndReturnSum(filePath, true);

            Assert.Equal(208, result);
        }


        [Fact]
        public void PartA_Actual()
        {
            var sut = new DayFourteen();
            var result = sut.PartA();

            Assert.Equal("17481577045893", result);
        }

        [Fact]
        public void PartB_Actual()
        {
            var sut = new DayFourteen();
            var result = sut.PartB();

            Assert.Equal("4160009892257", result);
        }
    }
}