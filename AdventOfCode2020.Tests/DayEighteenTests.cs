using AdventOfCode2020.Eighteen;
using Xunit;

namespace AdventOfCode2020.Tests
{
    public class DayEighteenTests
    {
        [Theory]
        [InlineData("1 + 2 * 3 + 4 * 5 + 6", 71)]
        [InlineData("1 + (2 * 3) + (4 * (5 + 6))", 51)]
        [InlineData("2 * 3 + (4 * 5)", 26)]
        [InlineData("5 + (8 * 3 + 9 + 3 * 4 * 3)", 437)]
        [InlineData("5 * 9 * (7 * 3 * 3 + 9 * 3 + (8 + 6 * 4))", 12240)]
        [InlineData("((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2", 13632)]
        public void CalculateEquation(string input, long expected)
        {
            var sut = new DayEighteen();
            var result = sut.CalculateEquationLeftToRight(input);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("1 + 2 * 3 + 4 * 5 + 6", 231)]
        [InlineData("1 + (2 * 3) + (4 * (5 + 6))", 51)]
        [InlineData("2 * 3 + (4 * 5)", 46)]
        [InlineData("5 + (8 * 3 + 9 + 3 * 4 * 3)", 1445)]
        [InlineData("5 * 9 * (7 * 3 * 3 + 9 * 3 + (8 + 6 * 4))", 669060)]
        [InlineData("((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2", 23340)]
        public void CalculateEquationAdditionFirst(string input, long expected)
        {
            var sut = new DayEighteen();
            var result = sut.CalculateEquationAdditionFirst(input);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void PartA_Actual()
        {
            var sut = new DayEighteen();
            var result = sut.PartA();

            Assert.Equal("280014646144", result);
        }

        [Fact]
        public void PartB_Actual()
        {
            var sut = new DayEighteen();
            var result = sut.PartB();

            Assert.Equal("9966990988262", result);
        }
    }
}