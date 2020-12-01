using AdventOfCode2020.One;
using System.Collections.Generic;
using Xunit;

namespace AdventOfCode2020.Tests
{
    public class DayOneTests
    {
        [Fact]
        public void FindReportSumTwo()
        {
            var input = new List<int>()
            {
                1721,
                979,
                366,
                299,
                675,
                1456
            };

            var sut = new DayOne();
            var result = sut.FindReportSumTwo(input);

            Assert.Equal(514579, result);
        }

        [Fact]
        public void FindReportSumThree()
        {
            var input = new List<int>()
            {
                1721,
                979,
                366,
                299,
                675,
                1456
            };

            var sut = new DayOne();
            var result = sut.FindReportSumThree(input);

            Assert.Equal(241861950, result);
        }

        [Fact]
        public void PartA_Actual()
        {
            var sut = new DayOne();
            var result = sut.PartA();

            Assert.Equal("651651", result);
        }

        [Fact]
        public void PartB_Actual()
        {
            var sut = new DayOne();
            var result = sut.PartB();

            Assert.Equal("214486272", result);
        }
    }
}