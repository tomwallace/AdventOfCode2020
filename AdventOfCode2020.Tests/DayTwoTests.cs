using AdventOfCode2020.Two;
using Xunit;

namespace AdventOfCode2020.Tests
{
    public class DayTwoTests
    {
        [Fact]
        public void PasswordDataConstruction()
        {
            string input = "1-3 a: abcde";
            var passwordData = new PasswordData(input);

            Assert.Equal("abcde", passwordData.Password);
            Assert.Equal('a', passwordData.RequiredLetter);
            Assert.Equal(1, passwordData.LowerLimit);
            Assert.Equal(3, passwordData.UpperLimit);

            Assert.True(passwordData.IsValidSledRentalRules());
        }

        [Theory]
        [InlineData("1-3 a: abcde", true)]
        [InlineData("1-3 b: cdefg", false)]
        [InlineData("2-9 c: ccccccccc", true)]
        public void PasswordData_IsValidSledRentalRules(string input, bool expected)
        {
            var passwordData = new PasswordData(input);
            Assert.Equal(expected, passwordData.IsValidSledRentalRules());
        }

        [Theory]
        [InlineData("1-3 a: abcde", true)]
        [InlineData("1-3 b: cdefg", false)]
        [InlineData("2-9 c: ccccccccc", false)]
        public void PasswordData_IsValidTobogganRentalRules(string input, bool expected)
        {
            var passwordData = new PasswordData(input);
            Assert.Equal(expected, passwordData.IsValidTobogganRentalRules());
        }

        [Fact]
        public void PartA_Actual()
        {
            var sut = new DayTwo();
            var result = sut.PartA();

            Assert.Equal("454", result);
        }

        [Fact]
        public void PartB_Actual()
        {
            var sut = new DayTwo();
            var result = sut.PartB();

            Assert.Equal("649", result);
        }
    }
}