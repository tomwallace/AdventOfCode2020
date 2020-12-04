using AdventOfCode2020.Four;
using Xunit;

namespace AdventOfCode2020.Tests
{
    public class DayFourTests
    {
        [Fact]
        public void FindNumberOfValidPassports()
        {
            var filePath = @"Four\DayFourTestInputA.txt";
            var sut = new DayFour();
            var result = sut.FindNumberOfValidPassports(filePath);

            Assert.Equal(2, result);
        }

        [Fact]
        public void FindNumberOfValidPassportsByRules_Invalid()
        {
            var filePath = @"Four\DayFourTestInputB.txt";
            var sut = new DayFour();
            var result = sut.FindNumberOfValidPassportsByRules(filePath);

            Assert.Equal(0, result);
        }

        [Fact]
        public void FindNumberOfValidPassportsByRules_Valid()
        {
            var filePath = @"Four\DayFourTestInputC.txt";
            var sut = new DayFour();
            var result = sut.FindNumberOfValidPassports(filePath);

            Assert.Equal(4, result);
        }

        [Fact]
        public void PartA_Actual()
        {
            var sut = new DayFour();
            var result = sut.PartA();

            Assert.Equal("219", result);
        }

        [Fact]
        public void PartB_Actual()
        {
            var sut = new DayFour();
            var result = sut.PartB();

            Assert.Equal("127", result);
        }
    }
}