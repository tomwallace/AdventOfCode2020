using AdventOfCode2020.Thirteen;
using Xunit;

namespace AdventOfCode2020.Tests
{
    public class DayThirteenTests
    {
        [Fact]
        public void BusRoutes_BestTime()
        {
            long timestamp = 939;
            string buses = "7,13,x,x,59,x,31,19";

            var sut = new BusRoutes(timestamp, buses);
            var result = sut.FindBestBusWaitTime();

            Assert.Equal(295, result);
        }

        /*
         * The earliest timestamp that matches the list 17,x,13,19 is 3417.
67,7,59,61 first occurs at timestamp 754018.
67,x,7,59,61 first occurs at timestamp 779210.
67,7,x,59,61 first occurs at timestamp 1261476.
1789,37,47,1889 first occurs at timestamp 1202161486.
         */

        [Theory]
        [InlineData("7,13,x,x,59,x,31,19", 1068781)]
        [InlineData("17,x,13,19", 3417)]
        [InlineData("67,7,59,61", 754018)]
        [InlineData("67,x,7,59,61", 779210)]
        [InlineData("67,7,x,59,61", 1261476)]
        //[InlineData("1789,37,47,1889", 1202161486)]
        public void BusRoutes_FindEarliestTimeStampAllMatch(string buses, long expected)
        {
            var sut = new BusRoutes(0, buses);
            Assert.True(sut.IsValidTimestamp(expected));
            var result = sut.FindEarliestTimeStampAllMatch();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void PartA_Actual()
        {
            var sut = new DayThirteen();
            var result = sut.PartA();

            Assert.Equal("2947", result);
        }

        [Fact]
        public void PartB_Actual()
        {
            var sut = new DayThirteen();
            var result = sut.PartB();

            Assert.Equal("-1", result);
        }
    }
}