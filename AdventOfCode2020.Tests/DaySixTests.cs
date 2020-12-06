using AdventOfCode2020.Six;
using Xunit;

namespace AdventOfCode2020.Tests
{
    public class DaySixTests
    {
        [Fact]
        public void SumCustomFormQuestionAnswers()
        {
            string filePath = @"Six\DaySixTestInputA.txt";
            var sut = new DaySix();
            var result = sut.SumCustomFormUniqueQuestionAnswers(filePath);

            Assert.Equal(11, result);
        }

        [Fact]
        public void SumCustomFormAllYesAnswers()
        {
            string filePath = @"Six\DaySixTestInputA.txt";
            var sut = new DaySix();
            var result = sut.SumCustomFormAllYesAnswers(filePath);

            Assert.Equal(6, result);
        }

        [Fact]
        public void PartA_Actual()
        {
            var sut = new DaySix();
            var result = sut.PartA();

            Assert.Equal("6387", result);
        }

        [Fact]
        public void PartB_Actual()
        {
            var sut = new DaySix();
            var result = sut.PartB();

            Assert.Equal("3039", result);
        }
    }
}