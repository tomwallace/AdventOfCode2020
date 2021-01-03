using AdventOfCode2020.Thirteen;
using System.Linq;
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
            var product = result.First().Key * result.First().Value;
            Assert.Equal(295, product);
        }

        [Fact]
        public void BusRoutes_FindWhenAllBusesMatch()
        {
            string buses = "7,13,x,x,59,x,31,19";

            var sut = new BusRoutes(0, buses);
            var result = sut.FindWhenAllBusesMatch();
            Assert.Equal(1068781, result);
        }

        [Theory]
        [InlineData("7,13,x,x,59,x,31,19", 1068781)]
        [InlineData("17,x,13,19", 3417)]
        [InlineData("67,7,59,61", 754018)]
        [InlineData("67,x,7,59,61", 779210)]
        [InlineData("67,7,x,59,61", 1261476)]
        [InlineData("1789,37,47,1889", 1202161486)]
        public void BusRoutes_FindWhenAllBusesMatchTheory(string buses, long expected)
        {
            var sut = new BusRoutes(0, buses);
            var result = sut.FindWhenAllBusesMatch();

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

            Assert.Equal("526090562196173", result);
        }
    }
}